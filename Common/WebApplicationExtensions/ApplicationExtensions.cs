using Microsoft.Extensions.DependencyInjection;

namespace Common.WebApplicationExtensions;

public static class ApplicationExtensions
{
    public static async void ManageServiceInScope<T>(this IServiceProvider provider, Func<T, Task> callback)
    {
        using (var scope = provider.CreateScope())
        {
            var service = scope.ServiceProvider.GetService<T>();
            if (service != null)
                await callback(service);
        }
    }
}