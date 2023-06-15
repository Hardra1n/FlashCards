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

    public async Task RemoveCard(Card card)
    {
        await _repository.DeleteCard(card);
        _repository.SaveChanges();
    }

    public async Task RemoveCardList(CardList list)
    {
        await _repository.DeleteCardList(list);
        _repository.SaveChanges();
    }

    public async Task<CardList?> GetCardListById(long id)
    {
        var cardList = await _repository.GetCardListById(id);
        return cardList;
    }

    public async Task<IEnumerable<CardList>> GetCardLists()
    {
        var cardLists = await _repository.GetCardLists();
        return cardLists;
    }

    public async Task<IEnumerable<Card>?> GetCards(long cardListId)
    {
        var cards = await _repository.GetCards(cardListId);
        return cards;
    }

    public async Task<Card?> CreateCard(long listId, Card card)
    {
        var cardWithId = await _repository.InsertCard(listId, card);
        _repository.SaveChanges();
        return cardWithId;
    }

    public async Task<CardList?> CreateCardList(CardList list)
    {
        var cardList = await _repository.InsertCardList(list);
        _repository.SaveChanges();
        return cardList;
    }

    public async Task<Card?> UpdateCard(long listId, long cardId, Card card)
    {
        var updatedCard = await _repository.UpdateCard(listId, cardId, card);
        _repository.SaveChanges();
        return updatedCard;
    }

    public async Task<CardList?> UpdateCardList(long id, CardList list)
    {
        var updatedCardList = await _repository.UpdateCardList(id, list);
        _repository.SaveChanges();
        return updatedCardList;
    }

    public async Task<Card?> GetCardById(long cardListId, long cardId)
    {
        var cards = await _repository.GetCards(cardListId);
        return cards?.FirstOrDefault(card => card.Id == cardId);
    }
}
