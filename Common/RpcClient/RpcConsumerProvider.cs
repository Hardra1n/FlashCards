namespace Common.RpcClient;

public class RpcConsumerProvider : IRpcConsumerProvider
{
    private BaseRpcConsumerClient _client;

    public RpcConsumerProvider(BaseRpcConsumerClient client)
    {
        _client = client;
    }

    public Action<string> CancellationCallback => CastCancellationCallBack;

    private void CastCancellationCallBack(string correlationId)
    {
        _client.CancelPendingReply(correlationId);
    }


    public string GetQueueName()
    {
        return _client.QueueName;
    }

    public Task<RpcClientMessage<byte[]>> RegisterMessageWaiting(string correlationId)
    {
        return _client.AddPendingReply(correlationId);
    }
}