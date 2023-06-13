using SpacedRep.RpcClients;

namespace SpacedRep.Services;

public class RpcConsumerService : IHostedService
{
    private RpcConsumer _service;

    public RpcConsumerService(IConfiguration configuration)
    {
        _service = new RpcConsumer(configuration);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}