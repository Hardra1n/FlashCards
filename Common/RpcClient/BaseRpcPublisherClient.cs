using RabbitMQ.Client;

namespace Common.RpcClient;

public abstract class BaseRpcPublisherClient : BaseRpcClient
{
    protected const int MESSAGE_EXPIRATION_TIME = 20000;
    protected IRpcConsumerProvider Consumer;

    public BaseRpcPublisherClient(
        RpcClientConfiguration configuration,
        IRpcConsumerProvider consumer) : base(configuration)
    {
        Consumer = consumer;
    }

    public void SendMessage(
        Byte[] body,
        string remoteMethodName,
        IBasicProperties? props,
        string? correlationId = null)
    {
        if (props == null)
            props = CreateDefaultProperties(correlationId);

        if (props.Headers == null)
            props.Headers = new Dictionary<string, object>();

        props.Headers.Add(COMMON_HEADER_KEY, remoteMethodName);
        Channel.BasicPublish(
            exchange: string.Empty,
            routingKey: QueueName,
            mandatory: false,
            basicProperties: props,
            body: body);
    }

    public Task<RpcClientMessage<Byte[]>> SendRepliableMessage(
            Byte[] body,
            string remoteMethodName,
            string? correlationId = null,
            CancellationToken cancellationToken = default)
    {
        if (correlationId == null)
            correlationId = Guid.NewGuid().ToString();

        SendMessage(body, remoteMethodName, null, correlationId);

        cancellationToken.Register(() => Consumer.CancellationCallback(correlationId));

        var task = Consumer.RegisterMessageWaiting(correlationId);

        return task;
    }

    private IBasicProperties CreateDefaultProperties(string? correlationId)
    {
        var props = Channel.CreateBasicProperties();
        if (correlationId != null && correlationId != string.Empty)
            props.CorrelationId = correlationId;
        props.Expiration = MESSAGE_EXPIRATION_TIME.ToString();
        props.ReplyTo = Consumer.GetQueueName();
        return props;
    }
}