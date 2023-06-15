using FlashCards.Models;
namespace FlashCards.Models.Repositories
{
    public interface ICardListRepository
    {
        Task<IEnumerable<CardList>> GetCardLists();

        Task<CardList?> GetCardListById(long id);

        Task<CardList?> InsertCardList(CardList list);

        Task<CardList?> UpdateCardList(long id, CardList list);

        Task DeleteCardList(CardList list);

        public Task<IQueryable<Card>?> GetCards(long cardListId);

        public Task<Card?> InsertCard(long listId, Card card);

        public Task<Card?> UpdateCard(long listId, long cardId, Card card);

        public Task DeleteCard(Card card);

        public void SaveChanges();

        public void ClearChanges();
    }
}
