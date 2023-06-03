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

            if (context.Repititions.Count() == 0)
            {
                Repetition[] reps = new[]
                {
                    new Repetition() {CreatedOn = DateTime.Now, Stage = RepetitionStage.Created},
                    new Repetition() {CreatedOn = new DateTime(2022, 6, 3), Stage = RepetitionStage.OnStudy },
                    new Repetition() {CreatedOn = new DateTime(2024, 6, 3), Stage = RepetitionStage.Archived },
                };
                context.AddRange(reps);
                context.SaveChanges();
            }
        }
    }
}