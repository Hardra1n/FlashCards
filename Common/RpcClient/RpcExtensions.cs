using System.Reflection;
using RabbitMQ.Client.Events;

namespace Common.RpcClient;

public static class RpcExtensions
{
    public static IEnumerable<KeyValuePair<string, Action<BasicDeliverEventArgs>>> GetAttributeNamesAndMethods(object invoker)
    {
        var type = invoker.GetType();
        var methods = type.GetMethods(BindingFlags.NonPublic
            | BindingFlags.Instance
            | BindingFlags.Public);
        foreach (var method in methods)
        {
            var attribute = method.GetCustomAttribute<ConsumeHandlerAttribute>();
            if (attribute != null)
            {
                var action = method.CreateDelegate<Action<BasicDeliverEventArgs>>(invoker);
                var pair = new KeyValuePair<string, Action<BasicDeliverEventArgs>>(
                    attribute.HandlerName,
                    action);
                yield return pair;
            }
        }
    }
}
