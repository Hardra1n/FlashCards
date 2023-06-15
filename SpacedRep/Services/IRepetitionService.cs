using SpacedRep.Models;

namespace SpacedRep.Services;

public interface IRepetitionService
{
    Task<Repetition> CreateAsync();
    Task<IEnumerable<Repetition>> Read();
    Task<Repetition?> ReadAsync(long id);
    Task<Repetition?> UpdateAsync(Repetition rep);
    Task<bool> DeleteAsync(long id);
}