using FlashCards.Data;
using FlashCards.Models;
using FlashCards.Services;
using Microsoft.EntityFrameworkCore;

namespace FlashCards
{
    public class DbInitializer
    {
        public static void Initialize(IApplicationBuilder app)
        {
            FlashCardsDbContext context = app.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<FlashCardsDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            ICardListService service = app.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<ICardListService>();
            if (context.CardLists.Count() == 0)
            {
                foreach (var cardlist in GetDefaultCardLists())
                {
                    service.CreateCardList(cardlist);
                    foreach (var card in cardlist.Cards)
                    {
                        service.CreateCard(cardlist.Id, card);
                    }
                }
            }
        }

        private static List<CardList> GetDefaultCardLists()
        {

            List<CardList> list = new List<CardList>();
            list.AddRange(new[]{
                    new CardList
                    {
                        Name = "First deck name",
                        Description = "First deck desc"
                    },
                    new CardList
                    {
                        Name = "Second deck name",
                        Description = "Second desc"
                    },
                    new CardList
                    {
                        Name = "Third deck name"
                    }
                });

            list[0].Cards.Add(new Card()
            {
                FrontSide = "First deck first front",
                BackSide = "First deck first back"
            });
            list[0].Cards.Add(new Card()
            {
                FrontSide = "First deck second front",
                BackSide = "First deck second back"
            });
            list[1].Cards.Add(new Card()
            {
                FrontSide = "Second deck first front",
                BackSide = "Second deck first back"
            });
            list[1].Cards.Add(new Card()
            {
                FrontSide = "Second deck second front",
                BackSide = "Second deck second back"
            });
            return list;
        }
    }
}