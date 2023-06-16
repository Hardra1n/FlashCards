using FlashCards.Models.Repositories;

namespace FlashCards.Services;

public class CardListRpcService
{
    private ICardListRepository _repository;
    private CardListRpcService _service;

    public CardListRpcService(ICardListRepository repository, CardListRpcService service)
    {
        _repository = repository;
        _service = service;
    }
}