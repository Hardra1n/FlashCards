using FlashCards.Models;
namespace FlashCards.Models.Repositories
{
    public interface ICardListRepository
    {
        IQueryable<CardList> CardLists { get; }

        CardList InsertCardList(CardList list);

        CardList? UpdateCardList(long id, CardList list);

        void DeleteCardList(CardList list);
    }
}
