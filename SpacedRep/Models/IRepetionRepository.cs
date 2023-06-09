namespace SpacedRep.Models
{
    public interface IRepetitionRepository
    {
        Task<Repetition> InsertRepetition();
        Task<IEnumerable<Repetition>> GetAllRepetitions();
        Task<Repetition?> GetRepetitionById(long id);
        Task<Repetition?> UpdateRepetition(Repetition rep);
        Task<bool> DeleteRepetition(long id);
        void SaveChanges();
        void ClearChanges();
        Task<IEnumerable<Repetition?>> GetRepetitionById(long[] ids);
    }
}