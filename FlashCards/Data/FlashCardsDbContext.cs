using FlashCards.Models;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Data
{
    public class FlashCardsDbContext : DbContext
    {
        public FlashCardsDbContext(DbContextOptions opts) : base(opts) { }

        public DbSet<Card> Cards => Set<Card>();
    }
}