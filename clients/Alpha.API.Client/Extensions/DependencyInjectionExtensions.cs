using System.Net;
using Alpha.API.Client.Infrastructure;
using Alpha.API.Client.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alpha.API.Client.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddAlphaServicesClient(this IServiceCollection services, IConfiguration configuration)
    {
        var alphaAPIBaseUrl = configuration.GetValue<string>("AlphaAPI:BaseUrl");
        services.AddScoped<AlphaAPIHmacDelegatingHandler>();

        services
            .AddHttpClient<ICustomerService, CustomerService>(httpClient
                => httpClient.BaseAddress = new Uri(alphaAPIBaseUrl))
            .AddHttpMessageHandler<AlphaAPIHmacDelegatingHandler>()
            .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            });
        
        services
            .AddHttpClient<IReportService, ReportService>(httpClient
                => httpClient.BaseAddress = new Uri(alphaAPIBaseUrl))
            .AddHttpMessageHandler<AlphaAPIHmacDelegatingHandler>()
            .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            });

        return services;
    }
}
