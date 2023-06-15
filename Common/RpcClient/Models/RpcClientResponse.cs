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
}