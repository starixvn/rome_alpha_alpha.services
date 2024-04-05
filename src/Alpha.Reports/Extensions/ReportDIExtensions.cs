using System.Diagnostics.CodeAnalysis;
using Alpha.Reports.Configurations;
using Alpha.Reports.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alpha.Reports.Extensions;

[ExcludeFromCodeCoverage]
public static class ReportDIExtensions
{
    public static IServiceCollection AddReportServices(this IServiceCollection services, IConfiguration configuration)
        => services
            .Configure<ReportAppSettings>(configuration.GetSection(nameof(ReportAppSettings)))
            .AddSingleton<IMinimizeUMService, MinimizeUMService>()
            .AddScoped<IProductService, ProductService>()
            .AddScoped<IReportDateService, ReportDateService>()
            .AddScoped<IBalanceInfoService, BalanceInfoService>()
            .AddScoped<IOutstandingService, OutstandingService>()
            .AddScoped<IWinLossService, WinLossService>()
            .AddScoped<ITotalBetsService, TotalBetsService>();
}
