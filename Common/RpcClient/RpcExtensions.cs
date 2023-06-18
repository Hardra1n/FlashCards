using System.Reflection;
using RabbitMQ.Client.Events;

namespace Common.RpcClient;

public static class RpcExtensions
{
    public static IEnumerable<KeyValuePair<string, Action<BasicDeliverEventArgs>>> GetAttributeNamesAndMethods(object invoker)
    {
        var type = invoker.GetType();
        // var methods = type.GetMethods(BindingFlags.NonPublic
        //     | BindingFlags.Instance
        //     | BindingFlags.Public);
        List<MethodInfo> methods = new List<MethodInfo>();
        while (type != null && type != typeof(BaseRpcConsumerClient))
        {
            var methodsOfType = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            methods.AddRange(methodsOfType);
            type = type.BaseType;
        }
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
