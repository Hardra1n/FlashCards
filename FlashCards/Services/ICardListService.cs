using FlashCards.Models;

namespace FlashCards.Services;

public interface ICardListService
{
    Task<IEnumerable<CardList>> GetCardLists();

    Task<CardList?> GetCardListById(long id);

    Task<CardList?> CreateCardList(CardList list);

    Task<CardList?> UpdateCardList(long id, CardList list);

    void RemoveCardList(CardList list);

    public Task<IEnumerable<Card>?> GetCards(long cardListId);

    public Task<Card?> CreateCard(long listId, Card card);

    public Task<Card?> UpdateCard(long listId, long cardId, Card card);

    public void RemoveCard(Card card);
}