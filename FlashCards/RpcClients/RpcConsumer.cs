using Common;
using Common.RpcClient;
using FlashCards.Services;

namespace FlashCards.RpcClients;

public class RpcConsumer : BaseRpcConsumerClient
{
    private CardListRpcService _service;

    public RpcConsumer(IConfiguration configuration, CardListRpcService service)
        : base(configuration.GetSection("FlashCards").Get<RpcClientConfiguration>()!)
    {
        _service = service;
    }
}