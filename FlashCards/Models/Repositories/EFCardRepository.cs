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
    }
}