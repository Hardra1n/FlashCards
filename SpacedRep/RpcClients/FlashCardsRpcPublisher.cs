using Common.RpcClient;
using SpacedRep.Models;

namespace SpacedRep.RpcClients;

public class FlashCardsRpcPublisher : RpcPublisherClient
{
    public FlashCardsRpcPublisher(IConfiguration configuration, IRpcConsumerProvider provider)
        : base(configuration.GetSection("FlashCards").Get<RpcClientConfiguration>()!, provider) { }
}