using System.Diagnostics.CodeAnalysis;
using Alpha.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Alpha.Shared.Extensions;

[ExcludeFromCodeCoverage]
public static class SharedDIExtensions
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
        => services.AddSingleton<ICacheRefService, CacheRefService>();
}
