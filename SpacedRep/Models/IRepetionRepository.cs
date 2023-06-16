namespace SpacedRep.Models
{
    public interface IRepetitionRepository
    {
        Task<Repetition> InsertRepetition();
        Task<IEnumerable<Repetition>> GetAllRepetitions();
        Task<Repetition?> GetRepetitionById(long id);
        Task<Repetition?> UpdateRepetition(Repetition rep);
        Task<bool> DeleteRepetition(long id);
        Task SaveChanges();
        Task ClearChanges();
    }
}