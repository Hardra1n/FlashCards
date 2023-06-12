using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Common.RpcClient;

public abstract class BaseConsumerRpcClient : BaseRpcClient
{
    protected IDictionary<string, Action<BasicDeliverEventArgs>> HandlerDictionary
     = new Dictionary<string, Action<BasicDeliverEventArgs>>();

    public BaseConsumerRpcClient(RpcClientConfiguration configuration) : base(configuration)
    {
        PopulateHandlerDictianary();

        var consumer = new EventingBasicConsumer(Channel);
        consumer.Received += ConsumeFromMethodHeader;

        Channel.BasicConsume(
            queue: QueueName,
            autoAck: true,
            consumer: consumer);
    }

    private void PopulateHandlerDictianary()
    {
        var pairs = RpcExtensions.GetAttributeNamesAndMethods(this);
        foreach (var pair in pairs)
        {
            HandlerDictionary.TryAdd(pair.Key, pair.Value);
        }
    }

    private void ConsumeFromMethodHeader(object? sender, BasicDeliverEventArgs ea)
    {
        if (ea.BasicProperties.Headers.TryGetValue(COMMON_HEADER_NAME, out var value))
        {
            string? methodName = Encoding.ASCII.GetString((Byte[])value);
            if (methodName != null && HandlerDictionary.TryGetValue(methodName, out var action))
                action.Invoke(ea);
        }
    }

    protected void SendResponse(BasicDeliverEventArgs ea, Byte[] body)
    {
        var props = Channel.CreateBasicProperties();
        props.CorrelationId = ea.BasicProperties.CorrelationId;
        Channel.BasicPublish(string.Empty, ea.BasicProperties.ReplyTo, false, props, body);
    }
}