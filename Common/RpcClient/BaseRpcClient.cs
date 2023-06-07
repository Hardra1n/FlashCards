using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Common.RpcClient;

public abstract class BaseRpcClient : IDisposable
{
    protected IConnection Connection;
    protected IModel Channel;

    protected IDictionary<string, Action<BasicDeliverEventArgs>> _handlerDictionary
     = new Dictionary<string, Action<BasicDeliverEventArgs>>();

    public BaseRpcClient(string hostname, string myQueueName)
    {
        var factory = new ConnectionFactory() { HostName = hostname };
        Connection = factory.CreateConnection();
        Channel = Connection.CreateModel();
        Channel.QueueDeclare(
            queue: myQueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        PopulateHandlerDictianary();
        var consumer = new EventingBasicConsumer(Channel);
        consumer.Received += ConsumeFromMethodHeader;

        Channel.BasicConsume(
            queue: myQueueName,
            autoAck: true,
            consumer: consumer);
    }

    private void PopulateHandlerDictianary()
    {
        var pairs = RpcExtensions.GetAttributeNamesAndMethods(this);
        foreach (var pair in pairs)
        {
            _handlerDictionary.TryAdd(pair.Key, pair.Value);
        }
    }

    protected void ConsumeFromMethodHeader(object? sender, BasicDeliverEventArgs ea)
    {
        if (ea.BasicProperties.Headers.TryGetValue("method", out var value))
        {
            string? methodName = Encoding.ASCII.GetString((Byte[])value);
            if (methodName != null && _handlerDictionary.TryGetValue(methodName, out var action))
                action.Invoke(ea);
        }
    }


    public void Dispose()
    {
        Channel.Close();
        Connection.Close();
    }
}