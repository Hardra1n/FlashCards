using Common;
using Common.RpcClient;
using FlashCards.Services;

namespace FlashCards.RpcClients;

public class RpcConsumer : RpcConsumerClient
{
    private CardListRpcService _service => _provider.GetService<CardListRpcService>()!;

    private IServiceProvider _provider;

    public RpcConsumer(IConfiguration configuration, IServiceProvider provider)
        : base(configuration.GetSection("FlashCards").Get<RpcClientConfiguration>()!)
    {
        _provider = provider;
    }
}