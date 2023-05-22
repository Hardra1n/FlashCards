using FlashCards.Models;
namespace FlashCards.Models.Repositories
{
    public interface ICardListRepository
    {
        IQueryable<CardList> CardLists { get; }

        CardList? GetCardListById(long id);

        CardList InsertCardList(CardList list);

        CardList? UpdateCardList(long id, CardList list);

        void DeleteCardList(CardList list);

        public IQueryable<Card>? GetCards(long cardListId);

        public Card? InsertCard(long listId, Card card);

        public Card? UpdateCard(long listId, long cardId, Card card);

        public void DeleteCard(Card card);
    }
}
