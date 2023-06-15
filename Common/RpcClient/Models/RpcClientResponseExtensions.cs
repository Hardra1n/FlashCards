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

    public static RpcClientResponse<T> CastBodyTo<T>(this Encoding encoder, RpcClientResponse<Byte[]> responseToCast) where T : IParsable<T>
    {
        string body = encoder.GetString(responseToCast.Data);
        T parsedBody = T.Parse(body, null);
        var response = responseToCast.Copy<T>(parsedBody);
        return response;
    }

    public static RpcClientResponse<string> CastBodyToString(this Encoding encoder, RpcClientResponse<Byte[]> responseToCast)
    {
        string body = encoder.GetString(responseToCast.Data);
        var response = responseToCast.Copy<string>(body);
        return response;
    }
}