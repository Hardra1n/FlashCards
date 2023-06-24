using Common.RpcClient;
using SpacedRep.Models;

namespace SpacedRep.RpcClients;

public class FlashCardsRpcPublisher : RpcPublisherClient
{
    public FlashCardsRpcPublisher(IConfiguration configuration, IRpcConsumerProvider provider)
        : base(configuration.GetSection("FlashCards").Get<RpcClientConfiguration>()!, provider) { }

    public async Task<bool> SendRepetitionCreated(Repetition repetition, string correlationId)
    {
        var body = Encoder.GetBytes(repetition.Id.ToString());
        var rpcMessage = new RpcClientMessage<Byte[]>(body, correlationId);
        var isApproved = await SendApprovableReply(rpcMessage);
        return isApproved;
    }

    public async Task<bool> SendRepetitionDeletion(string correlationId)
    {
        var body = Encoder.GetBytes(true.ToString());
        var rpcMessage = new RpcClientMessage<Byte[]>(body, correlationId);
        var isApproved = await SendApprovableReply(rpcMessage);
        return isApproved;
    }
}