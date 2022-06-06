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
            .BuildMinimizeUM(configuration)
            .AddSingleton<IMinimizeUMService, MinimizeUMService>()
            .AddScoped<IProductService, ProductService>()
            .AddScoped<IReportDateService, ReportDateService>()
            .AddScoped<IBalanceInfoService, BalanceInfoService>()
            .AddScoped<IOutstandingService, OutstandingService>()
            .AddScoped<IWinLossService, WinLossService>();

    private static IServiceCollection BuildMinimizeUM(this IServiceCollection services, IConfiguration configuration)
    {
        var controllerNames = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);

        var minimizeUMConfig = configuration.GetSection($"{nameof(ReportAppSettings)}:{nameof(MinimizeUM)}");
        minimizeUMConfig
            .GetSection("GroupFunctions")
            .Get<GroupFunction[]>()
            .Where(f => f.IsActive)
            .ToList()
            .ForEach(group => group.Controllers.ToList().ForEach(function => controllerNames[function] = true));

        var minimizeUM = new MinimizeUM(
            minimizeUMConfig.GetValue<string>("StartTime"),
            minimizeUMConfig.GetValue<string>("EndTime"),
            controllerNames);

        return services.AddSingleton(minimizeUM);
    }
}
