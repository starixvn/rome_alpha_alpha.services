using System.Diagnostics.CodeAnalysis;
using System.Net;
using Alpha.API.Client.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alpha.API.Client.Extensions;

// Justification: This class only contains extension methods for DI registration and does not require code coverage.
[ExcludeFromCodeCoverage(Justification = "This class only contains extension methods for DI registration and does not require code coverage.")]
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddAlphaServicesClient(this IServiceCollection services, IConfiguration configuration)
    {
        var alphaAPIBaseUrl = configuration.GetValue<string>("AlphaAPIBaseUrl");

        services
            .AddHttpClient<ICustomerService, CustomerService>(httpClient
                => httpClient.BaseAddress = new Uri(alphaAPIBaseUrl))
            .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            });
        
        services
            .AddHttpClient<IOutstandingService, OutstandingService>(httpClient
                => httpClient.BaseAddress = new Uri(alphaAPIBaseUrl))
            .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            });

        services
            .AddHttpClient<IBalanceInfoService, BalanceInfoService>(httpClient
                => httpClient.BaseAddress = new Uri(alphaAPIBaseUrl))
            .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            });

        services
            .AddHttpClient<IWinLossService, WinLossService>(httpClient
                => httpClient.BaseAddress = new Uri(alphaAPIBaseUrl))
            .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            });

        services
            .AddHttpClient<IAutoTransferService, AutoTransferService>(httpClient
                => httpClient.BaseAddress = new Uri(alphaAPIBaseUrl))
            .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            });
            
        services
            .AddHttpClient<ITotalBetsService, TotalBetsService>(httpClient
                => httpClient.BaseAddress = new Uri(alphaAPIBaseUrl))
            .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            });

        return services;
    }
}
