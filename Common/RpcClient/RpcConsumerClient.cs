using RabbitMQ.Client.Events;

namespace Common.RpcClient;

public class RpcConsumerClient : BaseRpcConsumerClient
{
    private const string BASE_OPERATION_EXPIRATION_TIME = "20000";

    public RpcConsumerClient(RpcClientConfiguration configuration) : base(configuration)
    {

    }

    [ConsumeHandler(PING_HEADER_VALUE)]
    private void HandlePing(BasicDeliverEventArgs ea)
    {
        var props = Channel.CreateBasicProperties();
        props.CorrelationId = ea.BasicProperties.CorrelationId;
        props.Expiration = BASE_OPERATION_EXPIRATION_TIME;
        props.Headers = new Dictionary<string, object>();
        props.Headers.Add(COMMON_HEADER_KEY, REPLY_HEADER_VALUE);
        Channel.BasicPublish(
            string.Empty,
            ea.BasicProperties.ReplyTo,
            false,
            props,
            ea.Body);
    }

    [ConsumeHandler(APPROVE_HEADER_VALUE)]
    private void HandleApprove(BasicDeliverEventArgs ea)
    {
        AssignResultToPendingReply(ea.BasicProperties.CorrelationId, ea.ToRpcClientResponse());
    }

    [ConsumeHandler(REPLY_HEADER_VALUE)]
    private void HandleReply(BasicDeliverEventArgs ea)
    {
        AssignResultToPendingReply(ea.BasicProperties.CorrelationId, ea.ToRpcClientResponse());
    }

    [ConsumeHandler(APPROVABLE_REPLY_HEADER_VALUE)]
    private void HandleApprovableReply(BasicDeliverEventArgs ea)
    {
        AssignResultToPendingReply(ea.BasicProperties.CorrelationId, ea.ToRpcClientResponse());
    }
}