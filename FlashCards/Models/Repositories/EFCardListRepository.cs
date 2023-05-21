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

        public CardList? GetCardListById(long id)
            => context.CardLists
                .Include(cardList => cardList.Cards)
                .FirstOrDefault(cardList => cardList.Id == id);

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
            var listToUpdate = GetCardListById(id);
            if (listToUpdate != null)
            {
                listToUpdate.ShallowCopy(list);
                context.SaveChanges();
            }
            return listToUpdate;
        }

        public IQueryable<Card>? GetCards(long cardListId)
        {
            return GetCardListById(cardListId)?.Cards.AsQueryable();
        }

        public Card? InsertCard(Card card)
        {
            var cardList = GetCardListById(card.CardListId);
            if (cardList != null)
            {
                var entry = context.Add<Card>(card);
                context.SaveChanges();
                return entry.Entity;
            }
            return null;
        }

        public Card? UpdateCard(long cardId, Card card)
        {
            var cardList = GetCardListById(card.CardListId);
            if (cardList != null)
            {
                var cardToUpdate = cardList.Cards.FirstOrDefault(card => card.Id == cardId);
                if (cardToUpdate != null)
                {
                    cardToUpdate.ShallowCopy(card);
                    context.SaveChanges();
                    return cardToUpdate;
                }
            }
            return null;
        }

        public void DeleteCard(Card card)
        {
            context.Remove<Card>(card);
            context.SaveChanges();
        }
    }
}