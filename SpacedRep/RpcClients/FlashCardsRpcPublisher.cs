using Common.RpcClient;

namespace SpacedRep.RpcClients;

public class FlashCardsRpcPublisher : BaseRpcPublisherClient
{
    public FlashCardsRpcPublisher(IConfiguration configuration)
        : base(configuration.GetSection("FlashCards").Get<RpcClientConfiguration>()!) { }
}