using SpacedRep.Models;

namespace SpacedRep.Services;

public class RepetitionService : IRepetitionService
{
    private IRepetitionRepository _repository;
    public RepetitionService(IRepetitionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Repetition> CreateAsync()
    {
        var repetition = await _repository.CreateAsync();
        await _repository.SaveChanges();
        return repetition;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var isSuccess = await _repository.DeleteAsync(id);
        await _repository.SaveChanges();
        return isSuccess;
    }

    public async Task<IEnumerable<Repetition>> Read()
    {
        var repetitions = await _repository.Read();
        return repetitions;
    }

    public async Task<Repetition?> ReadAsync(long id)
    {
        var repetition = await _repository.ReadAsync(id);
        return repetition;
    }

    public async Task<Repetition?> UpdateAsync(Repetition rep)
    {
        var repetition = await _repository.UpdateAsync(rep);
        await _repository.SaveChanges();
        return repetition;
    }
}