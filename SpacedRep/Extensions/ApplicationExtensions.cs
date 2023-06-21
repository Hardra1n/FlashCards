namespace SpacedRep.Extensions;

public static class ApplicationExtensions
{
    public static void ManageServiceInScope<T>(this IServiceProvider provider, Action<T> callback)
    {
        using (var scope = provider.CreateScope())
        {
            var service = scope.ServiceProvider.GetService<T>();
            if (service != null)
                callback(service);
        }
    }
}