using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alpha.Shared.Infrastructure;

public static class HmacAuthenticationExtensions
{
    public static IServiceCollection AddHMACAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddAuthentication("HMAC")
            .AddScheme<HmacAuthenticationSchemeOptions, HmacAuthenticationHandler>("HMAC", opt =>
            {
                opt.DisableHMACAuthentication = configuration.GetValue<bool>("HmacAuthenticationSchemeOptions:DisableHMACAuthentication");
                opt.AppId = configuration.GetValue<string>("HmacAuthenticationSchemeOptions:AppId");
                opt.APIKey = configuration.GetValue<string>("HmacAuthenticationSchemeOptions:APIKey");
                opt.RequestMaxAgeInSeconds = configuration.GetValue<long>("HmacAuthenticationSchemeOptions:RequestMaxAgeInSeconds");
            });

        return services;
    }
}
