using FlashCards.Models;
using FlashCards.Models.Repositories;
using FlashCards.RpcClients;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Services;

public class CardListApiService
{
    private ICardListRepository _repository;

    private SpacedRepRpcPublisher _rpcPublisher;

    public CardListApiService(ICardListRepository repository, SpacedRepRpcPublisher rpcPublisher)
    {
        _repository = repository;
        _rpcPublisher = rpcPublisher;
    }

    public async Task<bool> RemoveCard(long listId, long cardId)
    {
        var result = await _repository.DeleteCard(listId, cardId);
        _repository.SaveChanges();
        return result;
    }

    public async Task<bool> RemoveCardList(long listId)
    {
        var result = await _repository.DeleteCardList(listId);
        _repository.SaveChanges();
        return result;
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
        try
        {
            var response = await _rpcPublisher.SendCardCreation();
            card.SpacedRepetitionId = response.Data;
            var insertedCard = await _repository.InsertCard(listId, card);
            _rpcPublisher.SendApprove(response.CorrelationId, insertedCard != null);
            if (insertedCard == null)
                throw new Exception();

            _repository.SaveChanges();
            return insertedCard;
        }
        catch
        {
            return null;
        }
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
