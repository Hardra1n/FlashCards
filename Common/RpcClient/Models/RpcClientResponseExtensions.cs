using System.Text;
using RabbitMQ.Client.Events;

namespace Common.RpcClient;

public static class RpcClientResponseExtensions
{
    public static RpcClientResponse<Byte[]> ToRpcClientResponse(this BasicDeliverEventArgs ea)
    {
        var rpcClientResponse = new RpcClientResponse<Byte[]>(ea.Body.ToArray());
        if (ea.BasicProperties.Headers.TryGetValue("status", out var value)
            && Boolean.TryParse(Encoding.ASCII.GetString((Byte[])value), out var isSuccess))
        {
            rpcClientResponse.IsSuccess = isSuccess;
        }
        return rpcClientResponse;
    }
}