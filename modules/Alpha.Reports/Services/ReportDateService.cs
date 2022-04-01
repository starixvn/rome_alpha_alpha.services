using Alpha.Reports.Configurations;
using Alpha.Reports.Criteria;
using Alpha.Reports.Models;
using Fanex.Caching;
using Fanex.Data.Repository;
using Microsoft.Extensions.Options;

namespace Alpha.Reports.Services;

public interface IReportDateService
{
    Task<DateTime> GetMoveBetsDayAsync();

    Task<BalanceUpDayAndMoveBetsDay> GetBalanceUpDayAndMoveBetsDayAsync();
}

public class ReportDateService : IReportDateService
{
    private const string BalanceUpDayAndMoveBetsDayCacheKey = "Alpha.Reports.Services.ReportDateService.BalanceUpDayAndMoveBetsDay";

    private readonly ICacheService cacheService;
    private readonly ReportAppSettings appSettings;
    private readonly IDynamicRepository dynamicRepository;

    public ReportDateService(
        ICacheService cacheService,
        IOptions<ReportAppSettings> options,
        IDynamicRepository dynamicRepository)
    {
        this.cacheService = cacheService;
        appSettings = options.Value;
        this.dynamicRepository = dynamicRepository;
    }

    public async Task<DateTime> GetMoveBetsDayAsync()
        => (await GetBalanceUpDayAndMoveBetsDayAsync()).MoveBetsDay;

    public Task<BalanceUpDayAndMoveBetsDay> GetBalanceUpDayAndMoveBetsDayAsync()
        => cacheService.GetOrSetAsync<BalanceUpDayAndMoveBetsDay>(
            BalanceUpDayAndMoveBetsDayCacheKey,
            () => dynamicRepository.GetAsync<BalanceUpDayAndMoveBetsDay>(new GetBalanceUpDayAndMoveBetsDayCriteria()),
            new CacheItemOptions().SetAbsoluteExpiration(
                TimeSpan.FromSeconds(appSettings.BalanceUpDayAndMoveBetsDayCacheInSeconds)));
}
