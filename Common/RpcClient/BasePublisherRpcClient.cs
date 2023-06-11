using System.Collections.Concurrent;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Common.RpcClient;

public abstract class BasePublisherRpcClient : BaseRpcClient
{
    private readonly string _consumerQueueName;

    protected ConcurrentDictionary<string, TaskCompletionSource<Byte[]>> taskCollection
        = new ConcurrentDictionary<string, TaskCompletionSource<Byte[]>>();

    public BasePublisherRpcClient(string hostname, string queueName) : base(hostname, queueName)
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

        props.Headers.Add(COMMON_HEADER_NAME, remoteMethodName);
        Channel.BasicPublish(string.Empty, QueueName, false, props, body);
    }

    public Task<Byte[]> SendRepliableMessage(Byte[] body, string remoteMethodName)
    {
        TaskCompletionSource<Byte[]> taskSource = new TaskCompletionSource<Byte[]>();
        var props = Channel.CreateBasicProperties();
        string messageId = Guid.NewGuid().ToString();
        props.CorrelationId = messageId;
        props.ReplyTo = _consumerQueueName;
        SendMessage(body, remoteMethodName, props);
        taskCollection.TryAdd(messageId, taskSource);
        return taskSource.Task;
    }

    private void ConsumeHandler(object? sender, BasicDeliverEventArgs ea)
    {
        if (taskCollection.TryGetValue(ea.BasicProperties.CorrelationId, out var value))
        {
            value.SetResult(ea.Body.ToArray());
        }
    }
}