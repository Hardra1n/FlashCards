using SpacedRep.Models;
using SpacedRep.Models.Remote;
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
            var isApproved = await _publisher.SendRepetitionCreated(repetition.ToSendRepetitionDto(), correlationId);
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

    public async Task DeleteRepetition(string correlationId, long repetitionId)
    {
        try
        {
            var result = await _repository.DeleteRepetition(repetitionId);
            if (!result)
                throw new Exception();
            var remoteResult = await _publisher.SendRepetitionDeletion(correlationId);
            if (!remoteResult)
                throw new Exception();
            _repository.SaveChanges();
        }
        catch
        {
            _publisher.SendReplyRefuse(correlationId);
        }
    }

    internal async Task GetRepetition(string correlationId, long repetitionId)
    {
        try
        {
            var repetition = await _repository.GetRepetitionById(repetitionId);
            if (repetition == null)
                throw new Exception();
            _publisher.SendRepetitionGetting(repetition, correlationId);
        }
        catch
        {
            _publisher.SendReplyRefuse(correlationId);
        }
    }

    internal async Task GetRepetitions(string correlationId, long[] repetitionsId)
    {
        try
        {
            var repetitions = await _repository.GetRepetitionById(repetitionsId);
            List<SendRepetitionDto> dtos = new List<SendRepetitionDto>();
            foreach (var rep in repetitions)
            {
                if (rep == null)
                    throw new Exception();
                dtos.Add(rep.ToSendRepetitionDto());
            }
            _publisher.SendRepetitionsGetting(dtos, correlationId);
        }
        catch
        {
            _publisher.SendReplyRefuse(correlationId);
        }
    }
}