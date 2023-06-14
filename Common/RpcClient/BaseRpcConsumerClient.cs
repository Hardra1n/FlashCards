using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Common.RpcClient;

public abstract class BaseRpcConsumerClient : BaseRpcClient
{
    protected IDictionary<string, Action<BasicDeliverEventArgs>> HandlerDictionary
     = new Dictionary<string, Action<BasicDeliverEventArgs>>();

    public BaseRpcConsumerClient(RpcClientConfiguration configuration) : base(configuration)
    {
        PopulateHandlerDictianary();

        var consumer = new EventingBasicConsumer(Channel);
        consumer.Received += Consume;

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

    private void Consume(object? sender, BasicDeliverEventArgs ea)
    {
        // Handle request if COMMON_HEADER_KEY exist in properties
        if (ea.BasicProperties.Headers.TryGetValue(COMMON_HEADER_KEY, out var value))
        {
            string? methodName = Encoding.ASCII.GetString((Byte[])value);
            ConsumeFromCommonHeaderKey(ea, methodName);
            return;
        }
    }


    private void ConsumeFromCommonHeaderKey(BasicDeliverEventArgs ea, string? value)
    {
        if (value != null && HandlerDictionary.TryGetValue(value, out var action))
            action.Invoke(ea);
    }


    protected void SendResponse(BasicDeliverEventArgs ea, Byte[] body)
    {
        var props = Channel.CreateBasicProperties();
        props.CorrelationId = ea.BasicProperties.CorrelationId;
        Channel.BasicPublish(string.Empty, ea.BasicProperties.ReplyTo, false, props, body);
    }

    [ConsumeHandler("ping")]
    public void PingHandler(BasicDeliverEventArgs ea)
    {
        SendResponse(ea, ea.Body.ToArray());
    }
}