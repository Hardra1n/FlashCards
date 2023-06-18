using Common.RpcClient;

namespace SpacedRep.RpcClients;

public class RpcConsumerWebProvider : RpcConsumerProvider
{
    public RpcConsumerWebProvider(IServiceProvider provider)
        : base(provider.GetService<RpcConsumer>()!) { }
}