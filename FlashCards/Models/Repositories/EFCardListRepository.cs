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

        public IEnumerable<CardList> GetCardLists() => context.CardLists;

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

        public Card? InsertCard(long listId, Card card)
        {
            var cardList = GetCardListById(listId);
            if (cardList != null)
            {
                cardList.Cards.Add(card);
                context.SaveChanges();
                return card;
            }
            return null;
        }

        public Card? UpdateCard(long listId, long cardId, Card card)
        {
            var cardList = GetCardListById(listId);
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