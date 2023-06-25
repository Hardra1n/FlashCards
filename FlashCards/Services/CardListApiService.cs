using FlashCards.Models;
using FlashCards.Models.Dtos;
using FlashCards.Models.Dtos.Remote;
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
        try
        {
            var cards = await _repository.GetCards(listId);
            var cardToRemove = cards?.FirstOrDefault(card => card.Id == cardId);
            if (cardToRemove == null)
                throw new Exception();

            var remoteResult = await _rpcPublisher.SendCardDeletion(cardToRemove.SpacedRepetitionId);
            var result = await _repository.DeleteCard(listId, cardId);
            if (remoteResult.Data == false || result == false)
                throw new Exception();
            _repository.SaveChanges();
            _rpcPublisher.SendApprove(remoteResult.CorrelationId);
            return result;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> RemoveCardList(long listId)
    {
        var result = await _repository.DeleteCardList(listId);
        _repository.SaveChanges();
        return result;
    }

    // GetCardListDto create
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

    // Enumberable of GetCardDto
    public async Task<IEnumerable<GetCardDto>?> GetCards(long cardListId)
    {
        try
        {
            var cards = await _repository.GetCards(cardListId);
            if (cards == null)
                throw new Exception();
            var idArray = cards.Select(card => card.SpacedRepetitionId);
            var response = await _rpcPublisher.SendCardsGetting(idArray.ToArray());
            var resultDtos = cards.Select(card => card.ToGetCardDto(
            response.Data.First(rep => rep.Id == card.SpacedRepetitionId)));
            return resultDtos;
        }
        catch
        {
            return null;
        }
    }

    public async Task<GetCardDto?> CreateCard(long listId, Card card)
    {
        try
        {
            var response = await _rpcPublisher.SendCardCreation();
            card.SpacedRepetitionId = response.Data.Id;
            var insertedCard = await _repository.InsertCard(listId, card);
            _rpcPublisher.SendApprove(response.CorrelationId, insertedCard != null);
            if (insertedCard == null)
                throw new Exception();

            _repository.SaveChanges();
            return insertedCard.ToGetCardDto(response.Data);
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

    public async Task<GetCardDto?> GetCardById(long cardListId, long cardId)
    {
        try
        {
            var cards = await _repository.GetCards(cardListId);
            var requiredCard = cards?.FirstOrDefault(card => card.Id == cardId);
            if (requiredCard == null)
                throw new Exception();
            var response = await _rpcPublisher.SendCardGetting(requiredCard.SpacedRepetitionId);
            return requiredCard.ToGetCardDto(response.Data);
        }
        catch
        {
            return null;
        }
    }
}
