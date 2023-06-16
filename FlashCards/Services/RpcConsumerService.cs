using FlashCards.RpcClients;

namespace FlashCards.Services;

public class RpcConsumerService : IHostedService
{
    private RpcConsumer _service;

    public RpcConsumerService(IConfiguration configuration, CardListRpcService service)
    {
        _service = new RpcConsumer(configuration, service);
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
