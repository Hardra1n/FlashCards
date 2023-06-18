using System.Text;
using RabbitMQ.Client.Events;

namespace Common.RpcClient;

public static class RpcClientResponseExtensions
{
    public static RpcClientMessage<Byte[]> ToRpcClientResponse(this BasicDeliverEventArgs ea)
    {
        var rpcClientResponse = new RpcClientMessage<Byte[]>(ea.Body.ToArray())
        {
            CorrelationId = ea.BasicProperties.CorrelationId
        };
        if (ea.BasicProperties.Headers.TryGetValue("status", out var status)
            && Boolean.TryParse(Encoding.ASCII.GetString((Byte[])status), out var isSuccess))
        {
            rpcClientResponse.IsSuccess = isSuccess;
        }
        return rpcClientResponse;
    }

    public static RpcClientMessage<T> CastBodyTo<T>(this Encoding encoder, RpcClientMessage<Byte[]> responseToCast) where T : IParsable<T>
    {
        string body = encoder.GetString(responseToCast.Data);
        T parsedBody = T.Parse(body, null);
        var response = responseToCast.Copy<T>(parsedBody);
        return response;
    }

    public static RpcClientMessage<string> CastBodyToString(this Encoding encoder, RpcClientMessage<Byte[]> responseToCast)
    {
        string body = encoder.GetString(responseToCast.Data);
        var response = responseToCast.Copy<string>(body);
        return response;
    }
}