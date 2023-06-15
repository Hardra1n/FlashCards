using Common.RpcClient;

namespace FlashCards.RpcClients;

public class SpacedRepRpcPublisher : BaseRpcPublisherClient
{
    public SpacedRepRpcPublisher(IConfiguration configuration)
     : base(configuration.GetSection("SpacedRep").Get<RpcClientConfiguration>()!) { }

    public async Task<RpcClientResponse<long>> SendCardCreation()
    {
        var response = await SendRepliableMessage(Array.Empty<Byte>(), "card-creation");
        return Encoder.CastBodyTo<long>(response);
    }
}