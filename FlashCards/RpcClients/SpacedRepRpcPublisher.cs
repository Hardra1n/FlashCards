using Common.RpcClient;

namespace FlashCards.RpcClients;

public class SpacedRepRpcPublisher : BaseRpcPublisherClient
{
    public SpacedRepRpcPublisher(IConfiguration configuration)
     : base(configuration.GetSection("SpacedRep").Get<RpcClientConfiguration>()!) { }
}