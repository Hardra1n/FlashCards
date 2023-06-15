namespace SpacedRep.Models
{
    public interface IRepetitionRepository
    {
        Task<Repetition> CreateAsync();
        Task<IEnumerable<Repetition>> Read();
        Task<Repetition?> ReadAsync(long id);
        Task<Repetition?> UpdateAsync(Repetition rep);
        Task<bool> DeleteAsync(long id);
        Task SaveChanges();
        Task ClearChanges();
    }
}