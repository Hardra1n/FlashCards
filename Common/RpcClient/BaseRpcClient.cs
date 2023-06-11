using System.Text;
using RabbitMQ.Client;

namespace Common.RpcClient;

public abstract class BaseRpcClient : IDisposable
{
    protected const string COMMON_HEADER_NAME = "method";

    protected IConnection Connection;

    protected IModel Channel;

    protected string QueueName;

    protected Encoding Encoder { get; set; } = Encoding.UTF8;

    public BaseRpcClient(string hostname, string queueName)
    {
        QueueName = queueName;
        var factory = new ConnectionFactory() { HostName = hostname };
        Connection = factory.CreateConnection();
        Channel = Connection.CreateModel();

        Channel.QueueDeclare(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    public void Dispose()
    {
        Channel.Close();
        Connection.Close();
    }
}