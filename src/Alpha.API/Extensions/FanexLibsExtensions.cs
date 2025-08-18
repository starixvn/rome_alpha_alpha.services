using System.Diagnostics.CodeAnalysis;
using Fanex.Logging;
using Fanex.Logging.Extensions;
using Fanex.Logging.Extensions.AspNetCore;
using Fanex.Logging.RabbitMQ;
using Fanex.Logging.Sentry;
using Fanex.RemotingRef;
using Sentry;

namespace Alpha.API.Extensions;

// S6513: Add a justification.
[ExcludeFromCodeCoverage(Justification = "This class only contains extension methods for DI registration and does not require code coverage.")]
public static class FanexLibsExtensions
{
    public static IServiceCollection AddRemotingRef(this IServiceCollection services, IConfiguration configuration)
    {
        RefDataTypeContainer
            .ConnectTo(new Uri(configuration["RemotingRefUrl"]))
            .FromApplication("Alpha.API");

        return services.AddSingleton<IRemotingRefService, RemotingRefService>();
    }

    public static IServiceCollection AddFanexLogging(this IServiceCollection services, IConfiguration configuration)
    {
        LogManager
            .SetDefaultLogCategory(configuration["Fanex.Logging:DefaultCategory"])
            .Use(GetLogEngines(configuration));

        ExceptionHandler
            .UseConfig(new LoggingConfiguration().GetHttpHeaderInfo());

        return services.AddSingleton(Logger.Log);
    }

    private static IEnumerable<ILogEngine> GetLogEngines(IConfiguration configuration)
    {
        yield return new RabbitMQLogging(configuration["Fanex.Logging:RabbitMQConnectionStrings"]);

        var sentryUrl = configuration["Fanex.Logging:SentryDsn"];
        if (!string.IsNullOrWhiteSpace(sentryUrl))
        {
            yield return new SentryLogging(new SentryEngineOptions
            {
                Dsn = new Dsn(sentryUrl)
            });
        }
    }

    public static IServiceCollection AddFanexCachingMemory(this IServiceCollection services)
        => services.AddSingleton<Fanex.Caching.ICacheService, Fanex.Caching.CacheService>();
}
