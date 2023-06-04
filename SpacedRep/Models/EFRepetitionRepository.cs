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

        public async Task<Repetition> CreateAsync()
        {
            var result = await _context.AddAsync(new Repetition());
            _context.SaveChanges();
            return result.Entity;

        }

        public async Task<bool> DeleteAsync(long id)
        {
            var repToDelete = await _context.FindAsync<Repetition>(id);
            if (repToDelete != null)
            {
                _context.Remove(repToDelete);
                _context.SaveChanges();
            }
            return repToDelete != null;
        }

        public IEnumerable<Repetition> Read()
        {
            return _context.Repetitions.AsEnumerable();
        }

        public async Task<Repetition?> ReadAsync(long id)
            => await _context.FindAsync<Repetition>(id);

        public async Task<Repetition?> UpdateAsync(Repetition rep)
        {
            var repToUpdate = await ReadAsync(rep.Id);
            if (repToUpdate != null)
            {
                repToUpdate.Copy(rep);
                _context.SaveChanges();
            }
            return repToUpdate;
        }
    }
}