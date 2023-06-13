using FlashCards.Models;
using FlashCards.Models.Repositories;
using FlashCards.RpcClients;

namespace FlashCards.Services;

public class CardListService : ICardListService
{
    private ICardListRepository _repository;

    private SpacedRepRpcPublisher _rpcPublisher;

    public CardListService(ICardListRepository repository, SpacedRepRpcPublisher rpcPublisher)
    {
        _repository = repository;
        _rpcPublisher = rpcPublisher;
    }

    public async void RemoveCard(Card card)
    {
        await Task.Run(() => _repository.DeleteCard(card));
    }

    public async void RemoveCardList(CardList list)
    {
        await Task.Run(() => _repository.DeleteCardList(list));
    }

    public async Task<CardList?> GetCardListById(long id)
    {
        return await _repository.GetCardListById(id);
    }

    public async Task<IEnumerable<CardList>> GetCardLists()
    {
        return await _repository.GetCardLists();
    }

    public async Task<IEnumerable<Card>?> GetCards(long cardListId)
    {
        return await _repository.GetCards(cardListId);
    }

    public async Task<Card?> CreateCard(long listId, Card card)
    {
        return await _repository.InsertCard(listId, card);
    }

    public async Task<CardList?> CreateCardList(CardList list)
    {
        return await _repository.InsertCardList(list);
    }

    public async Task<Card?> UpdateCard(long listId, long cardId, Card card)
    {
        return await _repository.UpdateCard(listId, cardId, card);
    }

    public async Task<CardList?> UpdateCardList(long id, CardList list)
    {
        return await _repository.UpdateCardList(id, list);
    }
}
