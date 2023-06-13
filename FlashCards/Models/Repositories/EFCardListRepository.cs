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

        public async Task<IEnumerable<CardList>> GetCardLists()
            => await Task.Run(() => context.CardLists);

        public async Task<CardList?> GetCardListById(long id)
        {
            return await Task.Run(() =>
                context.CardLists
                    .Include(cardList => cardList.Cards)
                    .FirstOrDefault(cardList => cardList.Id == id));
        }

        public async Task<CardList?> InsertCardList(CardList list)
        {
            var entity = await context.AddAsync(list);
            await context.SaveChangesAsync();
            return entity.Entity;
        }

        public async void DeleteCardList(CardList list)
        {
            context.Remove(list);
            await context.SaveChangesAsync();
        }

        public async Task<CardList?> UpdateCardList(long id, CardList list)
        {
            var listToUpdate = await GetCardListById(id);
            if (listToUpdate != null)
            {
                listToUpdate.ShallowCopy(list);
                await context.SaveChangesAsync();
            }
            return listToUpdate;
        }

        public async Task<IQueryable<Card>?> GetCards(long cardListId)
            => (await GetCardListById(cardListId))?.Cards.AsQueryable();


        public async Task<Card?> InsertCard(long listId, Card card)
        {
            var cardList = await GetCardListById(listId);
            if (cardList != null)
            {
                cardList.Cards.Add(card);
                await context.SaveChangesAsync();
                return card;
            }
            return null;
        }

        public async Task<Card?> UpdateCard(long listId, long cardId, Card card)
        {
            var cardList = await GetCardListById(listId);
            if (cardList != null)
            {
                var cardToUpdate = cardList.Cards.FirstOrDefault(card => card.Id == cardId);
                if (cardToUpdate != null)
                {
                    cardToUpdate.ShallowCopy(card);
                    await context.SaveChangesAsync();
                    return cardToUpdate;
                }
            }
            return null;
        }

        public async void DeleteCard(Card card)
        {
            context.Remove<Card>(card);
            await context.SaveChangesAsync();
        }
    }
}