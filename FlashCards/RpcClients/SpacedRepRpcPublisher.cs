using Common.RpcClient;
using FlashCards.Extensions;
using FlashCards.Models.Dtos.Remote;

namespace FlashCards.RpcClients;

public class SpacedRepRpcPublisher : RpcPublisherClient
{
    public SpacedRepRpcPublisher(IConfiguration configuration, IRpcConsumerProvider provider)
     : base(configuration.GetSection("SpacedRep").Get<RpcClientConfiguration>()!, provider) { }

    public async Task<RpcClientMessage<RecieveRepetitionDto>> SendCardCreation()
    {
        var response = await SendRepliableMessage(Array.Empty<Byte>(), "card-creation-request");
        var convertedResponse = response.Copy<RecieveRepetitionDto>(response.Data.ToRecieveRepetitionDto());
        return convertedResponse;
    }

    public async Task<RpcClientMessage<bool>> SendCardDeletion(long spacedRepetitionId)
    {
        var body = Encoder.GetBytes(spacedRepetitionId.ToString());
        var response = await SendRepliableMessage(body, "card-deletion-request");
        return Encoder.CastBodyToBoolean(response);
    }
}