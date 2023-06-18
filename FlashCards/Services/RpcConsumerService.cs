using FlashCards.RpcClients;

namespace FlashCards.Services;

public class RpcConsumerService : IHostedService
{
    private RpcConsumer _service;

    public RpcConsumerService(RpcConsumer service)
    {
        _service = service;
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
