using RabbitMQ.Client.Events;

namespace Common.RpcClient;

public class RpcClientResponse<T>
{
    public bool IsSuccess { get; set; }

    public T Data { get; set; }

    public RpcClientResponse(T data)
    {
        Data = data;
    }

    public RpcClientResponse<Y> Copy<Y>(Y data)
    {
        RpcClientResponse<Y> response = new(data);
        response.IsSuccess = this.IsSuccess;
        return response;
    }
}