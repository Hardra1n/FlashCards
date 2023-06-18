using FlashCards.Models.Repositories;
using FlashCards.RpcClients;

namespace FlashCards.Services;

public class CardListRpcService
{
    private ICardListRepository _repository;
    private SpacedRepRpcPublisher _publisher;

    public CardListRpcService(ICardListRepository repository, SpacedRepRpcPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }
}