using Microsoft.EntityFrameworkCore;
using SpacedRep.Models;

namespace SpacedRep.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IApplicationBuilder app)
        {
            SpacedRepDbContext context = app.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<SpacedRepDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (context.Repetitions.Count() == 0)
            {
                Repetition[] reps = new[]
                {
                    new Repetition() { Stage = RepetitionStage.Created },
                    new Repetition() { Stage = RepetitionStage.OnStudy },
                    new Repetition() { Stage = RepetitionStage.Archived },
                };
                context.AddRange(reps);
                context.SaveChanges();
            }
        }
    }
}