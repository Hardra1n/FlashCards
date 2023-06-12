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

    public BaseRpcClient(RpcClientConfiguration configuration)
    {
        QueueName = configuration.QueueName;
        var factory = new ConnectionFactory() { HostName = configuration.HostName };
        Connection = factory.CreateConnection();
        Channel = Connection.CreateModel();

        Channel.QueueDeclare(
            queue: QueueName,
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