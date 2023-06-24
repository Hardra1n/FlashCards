using Common.RpcClient;
using Common.WebApplicationExtensions;
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

    [ConsumeHandler("card-creation-request")]
    private void HandleCardCreationRequest(BasicDeliverEventArgs ea)
    {
        _provider.ManageServiceInScope<RepetitionRpcService>(async service =>
       {
           await service.CreateRepetition(ea.BasicProperties.CorrelationId);
       });
    }

    [ConsumeHandler("card-deletion-request")]
    private void HandleCardDeletionRequest(BasicDeliverEventArgs ea)
    {
        long repetitionId = long.Parse(Encoder.GetString(ea.Body.ToArray()));
        _provider.ManageServiceInScope<RepetitionRpcService>(async service =>
        {
            await service.DeleteRepetition(ea.BasicProperties.CorrelationId, repetitionId);
        });
    }
}