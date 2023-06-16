using SpacedRep.Models;

namespace SpacedRep.Services;

public class RepetitionService : IRepetitionService
{
    private IRepetitionRepository _repository;
    public RepetitionService(IRepetitionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Repetition> CreateRepetition()
    {
        var repetition = await _repository.InsertRepetition();
        await _repository.SaveChanges();
        return repetition;
    }

    public async Task<bool> RemoveRepetition(long id)
    {
        var isSuccess = await _repository.DeleteRepetition(id);
        await _repository.SaveChanges();
        return isSuccess;
    }

    public async Task<IEnumerable<Repetition>> GetAllRepetitions()
    {
        var repetitions = await _repository.GetAllRepetitions();
        return repetitions;
    }

    public async Task<Repetition?> GetRepetitionById(long id)
    {
        var repetition = await _repository.GetRepetitionById(id);
        return repetition;
    }

    public async Task<Repetition?> UpdateRepetition(Repetition rep)
    {
        var repetition = await _repository.UpdateRepetition(rep);
        await _repository.SaveChanges();
        return repetition;
    }
}