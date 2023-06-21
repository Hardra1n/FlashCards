using SpacedRep.Models;
using SpacedRep.RpcClients;

namespace SpacedRep.Services;

public class RepetitionRpcService
{
    private IRepetitionRepository _repository;

    private FlashCardsRpcPublisher _publisher;

    public RepetitionRpcService(IRepetitionRepository repository, FlashCardsRpcPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }

    public async Task CreateRepetition(string correlationId)
    {
        try
        {
            var repetition = await _repository.InsertRepetition();
            if (repetition == null)
                throw new Exception();
            _repository.SaveChanges();
            var isApproved = await _publisher.SendRepetitionCreated(repetition, correlationId);
            if (!isApproved)
            {
                await _repository.DeleteRepetition(repetition.Id);
                _repository.SaveChanges();
            }
        }
        catch
        {
            _publisher.SendReplyRefuse(correlationId);
        }
    }
}