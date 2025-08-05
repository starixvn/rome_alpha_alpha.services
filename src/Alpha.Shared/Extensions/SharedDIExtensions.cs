using Alpha.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Alpha.Shared.Extensions;

public static class SharedDIExtensions
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
        => services.AddSingleton<ICacheRefService, CacheRefService>();
}
