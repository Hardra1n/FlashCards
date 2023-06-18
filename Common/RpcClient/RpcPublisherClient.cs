namespace Common.RpcClient;

public class RpcPublisherClient : BaseRpcPublisherClient
{
    public RpcPublisherClient(RpcClientConfiguration configuration, IRpcConsumerProvider consumer)
        : base(configuration, consumer) { }

    public async Task<bool> SendPing()
    {
        string message = Guid.NewGuid().ToString();
        var body = Encoder.GetBytes(message);
        try
        {
            var response = await SendRepliableMessage(body, PING_HEADER_VALUE);
            string receivedMessage = Encoder.GetString(response.Data);
            return receivedMessage == message;
        }
        catch
        {
            return false;
        }
    }

    public void SendApprove(string? correlationId, bool isApproved = true)
    {
        var body = Encoder.GetBytes(isApproved.ToString());
        SendMessage(body, APPROVE_HEADER_VALUE, null, correlationId);
    }

    public void SendReply(RpcClientMessage<Byte[]> message)
    {
        SendMessage(message.Data, REPLY_HEADER_VALUE, null, message.CorrelationId);
    }

    public async Task<bool> SendApprovableReply(RpcClientMessage<Byte[]> message)
    {
        var response = await SendRepliableMessage(
            message.Data,
            APPROVABLE_REPLY_HEADER_VALUE,
            message.CorrelationId);
        bool isApproved = Boolean.Parse(Encoder.GetString(response.Data));
        return isApproved;
    }

    public new Task<RpcClientMessage<Byte[]>> SendRepliableMessage(
            Byte[] body,
            string remoteMethodName,
            string? correlationId = null,
            CancellationToken cancellationToken = default)
    {
        if (cancellationToken == default)
            cancellationToken = new TimeoutToken(MESSAGE_EXPIRATION_TIME).Token;
        return base.SendRepliableMessage(body, remoteMethodName, correlationId, cancellationToken);
    }
}