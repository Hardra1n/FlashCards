using Common.RpcClient;
using SpacedRep.Extensions;
using SpacedRep.Models;
using SpacedRep.Models.Remote;

namespace SpacedRep.RpcClients;

public class FlashCardsRpcPublisher : RpcPublisherClient
{
    public FlashCardsRpcPublisher(IConfiguration configuration, IRpcConsumerProvider provider)
        : base(configuration.GetSection("FlashCards").Get<RpcClientConfiguration>()!, provider) { }

    public async Task<bool> SendRepetitionCreated(SendRepetitionDto repDto, string correlationId)
    {
        var body = repDto.ToByteArray();
        var rpcMessage = new RpcClientMessage<Byte[]>(body, correlationId);
        var isApproved = await SendApprovableReply(rpcMessage);
        return isApproved;
    }

    public async Task<bool> SendRepetitionDeletion(string correlationId)
    {
        var body = Encoder.GetBytes(true.ToString());
        var rpcMessage = new RpcClientMessage<Byte[]>(body, correlationId);
        var isApproved = await SendApprovableReply(rpcMessage);
        return isApproved;
    }

    internal void SendRepetitionGetting(Repetition repetition, string correlationId)
    {
        var body = repetition.ToSendRepetitionDto().ToByteArray();
        var rpcMessage = new RpcClientMessage<Byte[]>(body, correlationId);
        SendReply(rpcMessage);
        return;
    }

    internal void SendRepetitionsGetting(IEnumerable<SendRepetitionDto> dtos, string correlationId)
    {
        List<Byte> body = new List<byte>();
        foreach (var dto in dtos)
        {
            body.AddRange(dto.ToByteArray());
        }
        var rpcMessage = new RpcClientMessage<Byte[]>(body.ToArray(), correlationId);
        SendReply(rpcMessage);
        return;
    }
}