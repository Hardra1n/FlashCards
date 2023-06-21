using FlashCards.Models;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Data
{
    public class FlashCardsDbContext : DbContext
    {
        public FlashCardsDbContext(DbContextOptions opts) : base(opts) { }

        // public DbSet<Card> Cards => Set<Card>();

        public DbSet<CardList> CardLists => Set<CardList>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string CARD_TO_CARDLIST_FK_NAME = "CardListId";
            modelBuilder.Entity<Card>().Property<long>(CARD_TO_CARDLIST_FK_NAME);
            modelBuilder.Entity<Card>().HasIndex(CARD_TO_CARDLIST_FK_NAME);
            modelBuilder.Entity<CardList>()
                .HasMany(cl => cl.Cards)
                .WithOne()
                .HasForeignKey(CARD_TO_CARDLIST_FK_NAME)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}