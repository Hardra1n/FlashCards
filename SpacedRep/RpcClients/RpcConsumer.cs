using Common.RpcClient;
using RabbitMQ.Client.Events;
using SpacedRep.Services;

namespace SpacedRep.RpcClients;

public class RpcConsumer : RpcConsumerClient
{

    private IServiceProvider _provider;
    public RpcConsumer(IConfiguration configuration, IServiceProvider provider)
        : base(configuration.GetSection("SpacedRep").Get<RpcClientConfiguration>()!)
    {
        _provider = provider;
    }

    private RepetitionRpcService _service => _provider.GetService<RepetitionRpcService>()!;
}