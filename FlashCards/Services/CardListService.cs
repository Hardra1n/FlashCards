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

    public void RemoveCard(Card card)
    {
        _repository.DeleteCard(card);
    }

    public void RemoveCardList(CardList list)
    {
        _repository.DeleteCardList(list);
    }

    public CardList? GetCardListById(long id)
    {
        return _repository.GetCardListById(id);
    }

    public IEnumerable<CardList> GetCardLists()
    {
        return _repository.GetCardLists();
    }

    public IEnumerable<Card>? GetCards(long cardListId)
    {
        return _repository.GetCards(cardListId);
    }

    public Card? CreateCard(long listId, Card card)
    {
        return _repository.InsertCard(listId, card);
    }

    public CardList CreateCardList(CardList list)
    {
        return _repository.InsertCardList(list);
    }

    public Card? UpdateCard(long listId, long cardId, Card card)
    {
        return _repository.UpdateCard(listId, cardId, card);
    }

    public CardList? UpdateCardList(long id, CardList list)
    {
        return _repository.UpdateCardList(id, list);
    }
}
