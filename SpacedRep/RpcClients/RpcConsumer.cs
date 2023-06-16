using Common.RpcClient;
using SpacedRep.Services;

namespace SpacedRep.RpcClients;

public class RpcConsumer : BaseRpcConsumerClient
{
    private RepetitionRpcService _service;

    public RpcConsumer(IConfiguration configuration, RepetitionRpcService service)
        : base(configuration.GetSection("SpacedRep").Get<RpcClientConfiguration>()!)
    {
        _service = service;
    }
}