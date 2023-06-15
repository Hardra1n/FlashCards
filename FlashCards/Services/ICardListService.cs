using FlashCards.Models;

namespace FlashCards.Services;

public interface ICardListService
{
    Task<IEnumerable<CardList>> GetCardLists();

    Task<CardList?> GetCardListById(long id);

    Task<CardList?> CreateCardList(CardList list);

    Task<CardList?> UpdateCardList(long id, CardList list);

    Task RemoveCardList(CardList list);

    Task<IEnumerable<Card>?> GetCards(long cardListId);

    Task<Card?> GetCardById(long cardListId, long cardId);

    Task<Card?> CreateCard(long listId, Card card);

    Task<Card?> UpdateCard(long listId, long cardId, Card card);

    Task RemoveCard(Card card);
}