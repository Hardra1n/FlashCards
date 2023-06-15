namespace Common.RpcClient;

[System.AttributeUsage(System.AttributeTargets.Method)]
public class ConsumeHandlerAttribute : Attribute
{
    public readonly string HandlerName;
    public ConsumeHandlerAttribute(string handlerName)
    {
        HandlerName = handlerName;
    }
}