using System.Linq;
using FlashCards.Data;

namespace FlashCards.Models.Repositories
{
    public class EFCardRepository : ICardRepository
    {
        private FlashCardsDbContext context { get; set; }

        public EFCardRepository(FlashCardsDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Card> Cards => context.Cards;

        public Card InsertCard(Card card)
        {
            var cardList = context.Find<CardList>(card.CardListId);
            if (cardList != null)
            {
                context.Add(card);
                context.SaveChanges();
            }
            return card;
        }

        public Card? UpdateCard(long id, Card card)
        {
            var dbCard = context.Find<Card>(id);
            if (dbCard != null)
            {
                dbCard = card;
                context.SaveChanges();
            }
            return dbCard;
        }

        public void DeleteCard(Card card)
        {
            context.Remove(card);
            context.SaveChanges();
        }
    }
}