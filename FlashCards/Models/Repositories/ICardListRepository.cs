using FlashCards.Models;
namespace FlashCards.Models.Repositories
{
    public interface ICardListRepository
    {
        IQueryable<CardList> CardLists { get; }

        CardList AddCardList(CardList list);

        CardList? UpdateCardList(long id, CardList list);

        void RemoveCardList(CardList list);
    }
}
