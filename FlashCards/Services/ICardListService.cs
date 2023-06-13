using FlashCards.Models;

namespace FlashCards.Services;

public interface ICardListService
{
    IEnumerable<CardList> GetCardLists();

    CardList? GetCardListById(long id);

    CardList CreateCardList(CardList list);

    CardList? UpdateCardList(long id, CardList list);

    void RemoveCardList(CardList list);

    public IEnumerable<Card>? GetCards(long cardListId);

    public Card? CreateCard(long listId, Card card);

    public Card? UpdateCard(long listId, long cardId, Card card);

    public void RemoveCard(Card card);
}