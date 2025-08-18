using System.Diagnostics.CodeAnalysis;
using Alpha.Reports.Configurations;
using Alpha.Reports.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alpha.Reports.Extensions;

// Justification: This class contains only DI extension methods and does not require code coverage.
[ExcludeFromCodeCoverage(Justification = "This class contains only DI extension methods and does not require code coverage.")]
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
