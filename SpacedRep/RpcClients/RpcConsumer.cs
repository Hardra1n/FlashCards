using Common.RpcClient;

namespace SpacedRep.RpcClients;

public class RpcConsumer : BaseRpcConsumerClient
{
    public RpcConsumer(IConfiguration configuration)
        : base(configuration.GetSection("SpacedRep").Get<RpcClientConfiguration>()!) { }
}