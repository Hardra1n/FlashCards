using Microsoft.EntityFrameworkCore;
using SpacedRep.Models;

namespace SpacedRep.Data
{
    public class SpacedRepDbContext : DbContext
    {
        public SpacedRepDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Repetition> Repititions => Set<Repetition>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Repetition>()
                .Property(obj => obj.Stage)
                .HasConversion<string>();

        }
    }
}