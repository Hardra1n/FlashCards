using FlashCards.Data;
using FlashCards.Models;
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

            if (context.CardLists.Count() == 0)
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

                context.AddRange(list);
                context.SaveChanges();
            }
        }
    }
}