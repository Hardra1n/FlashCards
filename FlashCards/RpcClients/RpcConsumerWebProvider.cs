using Common.RpcClient;

namespace FlashCards.RpcClients;

public class RpcConsumerWebProvider : RpcConsumerProvider
{
    public RpcConsumerWebProvider(IServiceProvider provider)
        : base(provider.GetService<RpcConsumer>()!) { }
}