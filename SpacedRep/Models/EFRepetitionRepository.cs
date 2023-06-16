using SpacedRep.Data;

namespace SpacedRep.Models
{
    public class EFRepetitionRepository : IRepetitionRepository
    {
        private SpacedRepDbContext _context;

        public EFRepetitionRepository(SpacedRepDbContext context)
        {
            _context = context;
        }

        public async Task<Repetition> InsertRepetition()
        {
            var result = await _context.AddAsync(new Repetition());
            return result.Entity;

        }

        public async Task<bool> DeleteRepetition(long id)
        {
            var repToDelete = await _context.FindAsync<Repetition>(id);
            if (repToDelete != null)
            {
                _context.Remove(repToDelete);
            }
            return repToDelete != null;
        }

        public async Task<Repetition?> GetRepetitionById(long id)
            => await _context.FindAsync<Repetition>(id);


        public async Task<Repetition?> UpdateRepetition(Repetition rep)
        {
            var repToUpdate = await GetRepetitionById(rep.Id);
            if (repToUpdate != null)
            {
                repToUpdate.Copy(rep);
            }
            return repToUpdate;
        }

        public async Task SaveChanges()
        {
            await Task.Run(() => _context.SaveChanges());
        }

        public async Task ClearChanges()
        {
            await Task.Run(() => _context.ChangeTracker.Clear());
        }

        public async Task<IEnumerable<Repetition>> GetAllRepetitions()
        {
            return await Task.Run(() => _context.Repetitions.AsEnumerable());
        }
    }
}