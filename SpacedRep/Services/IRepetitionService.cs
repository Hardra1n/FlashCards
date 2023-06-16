using SpacedRep.Models;

namespace SpacedRep.Services;

public interface IRepetitionService
{
    Task<Repetition> CreateRepetition();
    Task<IEnumerable<Repetition>> GetAllRepetitions();
    Task<Repetition?> GetRepetitionById(long id);
    Task<Repetition?> UpdateRepetition(Repetition rep);
    Task<bool> RemoveRepetition(long id);
}