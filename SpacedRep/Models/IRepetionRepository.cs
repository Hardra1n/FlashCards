namespace SpacedRep.Models
{
    public interface IRepetitionRepository
    {
        Task<Repetition> CreateAsync();
        IEnumerable<Repetition> Read();
        Task<Repetition?> ReadAsync(long id);
        Task<Repetition?> UpdateAsync(Repetition rep);
        Task<bool> DeleteAsync(long id);
    }
}