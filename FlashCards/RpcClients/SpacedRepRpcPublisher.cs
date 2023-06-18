using Common.RpcClient;

namespace FlashCards.RpcClients;

public class SpacedRepRpcPublisher : RpcPublisherClient
{
    public SpacedRepRpcPublisher(IConfiguration configuration, IRpcConsumerProvider provider)
     : base(configuration.GetSection("SpacedRep").Get<RpcClientConfiguration>()!, provider) { }

    public async Task<RpcClientMessage<long>> SendCardCreation()
    {
        var response = await SendRepliableMessage(Array.Empty<Byte>(), "card-creation");
        return Encoder.CastBodyTo<long>(response);
    }
}