using System.Collections.Concurrent;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Common.RpcClient;

public abstract class BaseRpcPublisherClient : BaseRpcClient
{
    private const int MESSAGE_EXPIRATION_MILLISEC = 8000;
    private const int MESSAGE_WAITING_MILLISEC = 10000;
    private readonly string _consumerQueueName;

    protected ConcurrentDictionary<string, TaskCompletionSource<Byte[]>> taskCollection
        = new ConcurrentDictionary<string, TaskCompletionSource<Byte[]>>();

    public BaseRpcPublisherClient(RpcClientConfiguration configuration) : base(configuration)
    {
        var consumer = new EventingBasicConsumer(Channel);
        consumer.Received += ConsumeHandler;
        _consumerQueueName = Channel.QueueDeclare().QueueName;
        Channel.BasicConsume(_consumerQueueName, true, consumer);
    }

    public void SendMessage(Byte[] body, string remoteMethodName, IBasicProperties? props = null)
    {
        if (props == null)
            props = Channel.CreateBasicProperties();

        if (props.Headers == null)
            props.Headers = new Dictionary<string, object>();

        props.Headers.Add(COMMON_HEADER_KEY, remoteMethodName);
        Channel.BasicPublish(string.Empty, QueueName, false, props, body);
    }

    public Task<Byte[]> SendRepliableMessage(Byte[] body, string remoteMethodName, CancellationToken cancellationToken = default)
    {
        if (cancellationToken == default)
            cancellationToken = new TimeoutToken(MESSAGE_WAITING_MILLISEC).Token;
        TaskCompletionSource<Byte[]> taskSource
            = new TaskCompletionSource<Byte[]>();

        string correlationId = Guid.NewGuid().ToString();
        var props = Channel.CreateBasicProperties();
        props.Expiration = MESSAGE_EXPIRATION_MILLISEC.ToString();
        props.CorrelationId = correlationId;
        props.ReplyTo = _consumerQueueName;

        SendMessage(body, remoteMethodName, props);
        taskCollection.TryAdd(correlationId, taskSource);

        cancellationToken.Register(()
            => ChangeStateOnTokenCancellation(correlationId));

        return taskSource.Task;
    }

    private void ChangeStateOnTokenCancellation(string correlationId)
    {
        if (taskCollection.TryRemove(correlationId, out var value))
        {
            value.SetCanceled();
        }
    }

    private void ConsumeHandler(object? sender, BasicDeliverEventArgs ea)
    {
        if (taskCollection.TryRemove(ea.BasicProperties.CorrelationId, out var value))
        {
            value.SetResult(ea.Body.ToArray());
        }
    }

    public async Task<bool> Ping()
    {
        var body = Encoder.GetBytes(Guid.NewGuid().ToString());
        try
        {
            var response = await SendRepliableMessage(body, "ping");
            return Encoder.GetString(body) == Encoder.GetString(response);
        }
        catch
        {
            return false;
        }
    }
}