using SpacedRep.Models;
using SpacedRep.RpcClients;

namespace SpacedRep.Services;

public class RepetitionRpcService
{
    private IRepetitionRepository _repository;

    private FlashCardsRpcPublisher _publisher;

    public RepetitionRpcService(IRepetitionRepository repository, FlashCardsRpcPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }
}