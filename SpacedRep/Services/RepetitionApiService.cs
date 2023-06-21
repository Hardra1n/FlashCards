using SpacedRep.Models;
using SpacedRep.RpcClients;

namespace SpacedRep.Services;

public class RepetitionApiService
{
    private IRepetitionRepository _repository;

    private FlashCardsRpcPublisher _publisher;

    public RepetitionApiService(IRepetitionRepository repository, FlashCardsRpcPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }

    public async Task<Repetition> CreateRepetition()
    {
        var repetition = await _repository.InsertRepetition();
        _repository.SaveChanges();
        return repetition;
    }

    public async Task<bool> RemoveRepetition(long id)
    {
        var isSuccess = await _repository.DeleteRepetition(id);
        _repository.SaveChanges();
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
        _repository.SaveChanges();
        return repetition;
    }
}