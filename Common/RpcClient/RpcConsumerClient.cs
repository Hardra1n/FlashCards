using RabbitMQ.Client.Events;

namespace Common.RpcClient;

public class RpcConsumerClient : BaseRpcConsumerClient
{
    private const string BASE_OPERATION_EXPIRATION_TIME = "20000";

    public RpcConsumerClient(RpcClientConfiguration configuration) : base(configuration)
    {

    }

    [ConsumeHandler(ClientMethodValues.PING)]
    private void HandlePing(BasicDeliverEventArgs ea)
    {
        var props = Channel.CreateBasicProperties();
        props.CorrelationId = ea.BasicProperties.CorrelationId;
        props.Expiration = BASE_OPERATION_EXPIRATION_TIME;
        props.Headers = new Dictionary<string, object>();
        props.Headers.Add(COMMON_HEADER_KEY, ClientMethodValues.REPLY);
        Channel.BasicPublish(
            string.Empty,
            ea.BasicProperties.ReplyTo,
            false,
            props,
            ea.Body);
    }

    [ConsumeHandler(ClientMethodValues.APPROVE)]
    private void HandleApprove(BasicDeliverEventArgs ea)
    {
        AssignResultToPendingReply(ea.BasicProperties.CorrelationId, ea.ToRpcClientResponse());
    }

    [ConsumeHandler(ClientMethodValues.REPLY)]
    private void HandleReply(BasicDeliverEventArgs ea)
    {
        AssignResultToPendingReply(ea.BasicProperties.CorrelationId, ea.ToRpcClientResponse());
    }

    [ConsumeHandler(ClientMethodValues.APPROVABLE_REPLY)]
    private void HandleApprovableReply(BasicDeliverEventArgs ea)
    {
        AssignResultToPendingReply(ea.BasicProperties.CorrelationId, ea.ToRpcClientResponse());
    }

    [ConsumeHandler(ClientMethodValues.REPLY_REFUSE)]
    private void HandleReplyRefuse(BasicDeliverEventArgs ea)
    {
        CancelPendingReply(ea.BasicProperties.CorrelationId);
    }
}