using Alpha.Reports.Configurations;
using Alpha.Reports.Criteria;
using Alpha.Reports.Models;
using Fanex.Caching;
using Fanex.Data.Repository;
using Microsoft.Extensions.Options;

namespace Alpha.Reports.Services;

public interface IReportDateService
{
    Task<DateTime> GetMoveBetsDay();

    Task<BalanceUpDayAndMoveBetsDay> GetBalanceUpDayAndMoveBetsDay();

    Task<DateTime> GetLimitDate(int custId);
}

public class ReportDateService : IReportDateService
{
    private const string BalanceUpDayAndMoveBetsDayCacheKey = "Alpha.Reports.BalanceUpDayAndMoveBetsDay";
    private const string CustomerLimitDateCacheKey = "Alpha.Reports.CustomerLimitDate";

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

    public async Task<DateTime> GetMoveBetsDay()
        => (await GetBalanceUpDayAndMoveBetsDay()).MoveBetsDay;

    public Task<BalanceUpDayAndMoveBetsDay> GetBalanceUpDayAndMoveBetsDay()
        => cacheService.GetOrSetAsync(
            BalanceUpDayAndMoveBetsDayCacheKey,
            () => dynamicRepository.GetAsync<BalanceUpDayAndMoveBetsDay>(new GetBalanceUpDayAndMoveBetsDayCriteria()),
            new CacheItemOptions().SetAbsoluteExpiration(
                TimeSpan.FromSeconds(appSettings.BalanceUpDayAndMoveBetsDayCacheInSeconds)));

    public Task<DateTime> GetLimitDate(int custId)
        => cacheService.GetOrSetAsync(
            $"{CustomerLimitDateCacheKey}.{custId}",
            () => GetLimitDateFromDB(custId),
            new CacheItemOptions().SetAbsoluteExpiration(
                TimeSpan.FromSeconds(appSettings.CustomerLimitDateCacheInSeconds)));

    private async Task<DateTime> GetLimitDateFromDB(int custId)
    {
        var customerLimitDate = await dynamicRepository.GetAsync<CustomerLimitDate>(new GetLimitDateCriteria(custId));
        
        return customerLimitDate == null
            ? DateTime.MinValue
            : customerLimitDate.DateLimit;
    }
}
