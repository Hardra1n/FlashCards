namespace Common.RpcClient;

public interface IRpcConsumerProvider
{
    Action<string> CancellationCallback { get; }

    string GetQueueName();

    Task<RpcClientMessage<byte[]>> RegisterMessageWaiting(string correlationId);
}