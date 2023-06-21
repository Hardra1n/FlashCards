using RabbitMQ.Client.Events;

namespace Common.RpcClient;

public class RpcClientMessage<T>
{
    public T Data { get; set; }

    public string? CorrelationId { get; set; }

    public RpcClientMessage(T data, string? correlationId = null)
    {
        CorrelationId = correlationId;
        Data = data;
    }

    public RpcClientMessage<Y> Copy<Y>(Y data)
    {
        RpcClientMessage<Y> response = new(data);
        response.CorrelationId = this.CorrelationId;
        return response;
    }
}