using Common;
using Common.RpcClient;

namespace FlashCards.RpcClients;

public class RpcConsumer : BaseRpcConsumerClient
{
    public RpcConsumer(IConfiguration configuration)
        : base(configuration.GetSection("FlashCards").Get<RpcClientConfiguration>()!) { }
}