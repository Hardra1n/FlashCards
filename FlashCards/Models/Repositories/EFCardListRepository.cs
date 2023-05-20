using FlashCards.Data;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Models.Repositories
{
    public class EFCardListRepository : ICardListRepository
    {
        private FlashCardsDbContext context;

        public EFCardListRepository(FlashCardsDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<CardList> CardLists => context.CardLists.Include(cardList => cardList.Cards);

        public CardList InsertCardList(CardList list)
        {
            context.Add(list);
            context.SaveChanges();
            return list;
        }

        public void DeleteCardList(CardList list)
        {
            context.Remove(list);
            context.SaveChanges();
        }

        public CardList? UpdateCardList(long id, CardList list)
        {
            var listToUpdate = context.Find<CardList>(id);
            if (listToUpdate != null)
            {
                listToUpdate = list;
                context.SaveChanges();
            }
            return listToUpdate;
        }
    }
}