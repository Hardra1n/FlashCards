using System.Collections.Concurrent;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Common.RpcClient;

public abstract class BaseRpcConsumerClient : BaseRpcClient
{
    protected IDictionary<string, Action<BasicDeliverEventArgs>> HandlerDictionary
         = new Dictionary<string, Action<BasicDeliverEventArgs>>();

    protected ConcurrentDictionary<string,
        TaskCompletionSource<RpcClientMessage<byte[]>>> ReplyingDictionary = new();

    public BaseRpcConsumerClient(RpcClientConfiguration configuration) : base(configuration)
    {
        PopulateHandlerDictianary();

        var consumer = new EventingBasicConsumer(Channel);
        consumer.Received += HandleConsume;

        Channel.BasicConsume(
            queue: QueueName,
            autoAck: true,
            consumer: consumer);
    }

    private void PopulateHandlerDictianary()
    {
        var pairs = RpcExtensions.GetAttributeNamesAndMethods(this);
        foreach (var pair in pairs)
        {
            HandlerDictionary.TryAdd(pair.Key, pair.Value);
        }
    }

    private void HandleConsume(object? sender, BasicDeliverEventArgs ea)
    {
        // Handle request if COMMON_HEADER_KEY exist in properties
        if (ea.BasicProperties.Headers.TryGetValue(COMMON_HEADER_KEY, out var value))
        {
            string? methodName = Encoding.ASCII.GetString((Byte[])value);
            ConsumeFromCommonHeaderKey(ea, methodName);
            return;
        }
    }


    private void ConsumeFromCommonHeaderKey(BasicDeliverEventArgs ea, string? value)
    {
        if (value != null && HandlerDictionary.Remove(value, out var action))
            action.Invoke(ea);
    }

    public Task<RpcClientMessage<byte[]>> AddPendingReply(string correlationId)
    {
        TaskCompletionSource<RpcClientMessage<byte[]>> tcs = new();
        ReplyingDictionary.TryAdd(correlationId, tcs);
        return tcs.Task;
    }

    public bool AssignResultToPendingReply(string correlationId, RpcClientMessage<byte[]> result)
    {
        if (ReplyingDictionary.TryRemove(correlationId, out var tsc))
        {
            tsc.SetResult(result);
            return true;
        }
        return false;
    }

    public bool CancelPendingReply(string correlationId)
    {
        if (ReplyingDictionary.TryRemove(correlationId, out var tcs))
        {
            tcs.SetCanceled();
            return true;
        }
        return false;
    }
}