using Common.RpcClient;
using SpacedRep.Models;

namespace SpacedRep.RpcClients;

public class FlashCardsRpcPublisher : RpcPublisherClient
{
    public FlashCardsRpcPublisher(IConfiguration configuration, IRpcConsumerProvider provider)
        : base(configuration.GetSection("FlashCards").Get<RpcClientConfiguration>()!, provider) { }

    public async Task<bool> SendRepetitionCreated(Repetition repetition, string correlationId)
    {
        var rpcMessage = new RpcClientMessage<Byte[]>(Encoder.GetBytes(repetition.Id.ToString()), correlationId);
        var isApproved = await SendApprovableReply(rpcMessage);
        return isApproved;
    }
}