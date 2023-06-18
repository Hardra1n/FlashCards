using RabbitMQ.Client.Events;

namespace Common.RpcClient;

public class RpcClientMessage<T>
{
    public bool IsSuccess { get; set; }

    public T Data { get; set; }

    public string? CorrelationId { get; set; }

    public RpcClientMessage(T data)
    {
        Data = data;
    }

    public RpcClientMessage<Y> Copy<Y>(Y data)
    {
        RpcClientMessage<Y> response = new(data);
        response.IsSuccess = this.IsSuccess;
        response.CorrelationId = this.CorrelationId;
        return response;
    }
}