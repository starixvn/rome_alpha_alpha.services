using Alpha.Reports.Configurations;
using Alpha.Reports.Consts;
using Alpha.Reports.Criteria.WinLoss;
using Alpha.Reports.Models.WinLoss;
using Alpha.Shared.Enumerations;
using Alpha.Shared.Extensions;
using Alpha.Shared.Utils;
using Fanex.Caching;
using Fanex.Data.Repository;
using Microsoft.Extensions.Options;

namespace Alpha.Reports.Services;

public interface IWinLossService
{
    Task<WinLossBase> GetWinLoss(int custId, UserLevel custLevel, DateTime fromDate, DateTime toDate, string groupProduct);

    Task<WinLossBase> GetDownlineWinLoss(int custId, UserLevel custLevel, DateTime fromDate, DateTime toDate, string groupProduct, int downlineId);

    Task<IEnumerable<string>> GetTopUsername(int custId, UserLevel custLevel, DateTime fromDate, DateTime toDate, string groupProduct, int top);

    Task<EnteredResult> GetYesterdayEnteredSportResult();
}

public class WinLossService : IWinLossService
{
    private const int SportsbookProductId = 1;
    private const string EnteredResultsCacheKey = "Alpha.Reports.Services.WinLossService.EnteredResults";
    private readonly IProductService productService;
    private readonly IReportDateService reportDateService;
    private readonly ReportAppSettings appSettings;
    private readonly IDynamicRepository dynamicRepository;
    private readonly ICacheService cacheService;

    public WinLossService(
        IProductService productService,
        IReportDateService reportDateService,
        IOptions<ReportAppSettings> options,
        IDynamicRepository dynamicRepository,
        ICacheService cacheService)
    {
        this.productService = productService;
        this.reportDateService = reportDateService;
        appSettings = options.Value;
        this.dynamicRepository = dynamicRepository;
        this.cacheService = cacheService;
    }

    public async Task<WinLossBase> GetWinLoss(int custId, UserLevel custLevel, DateTime fromDate, DateTime toDate, string groupProduct)
    {
        if (custLevel.IsSuper())
        {
            return await GetSumWinLossMaster(custId, custLevel, fromDate, toDate, groupProduct, null);
        }

        if (custLevel.IsMaster())
        {
            return await GetSumWinLossAgent(custId, custLevel, fromDate, toDate, groupProduct, null);
        }

        return await GetSumWinLossMember(custId, custLevel, fromDate, toDate, groupProduct, null);
    }

    public async Task<WinLossBase> GetDownlineWinLoss(int custId, UserLevel custLevel, DateTime fromDate, DateTime toDate, string groupProduct, int downlineId)
    {
        if (custLevel.IsSuper())
        {
            return await GetSumWinLossMaster(custId, custLevel, fromDate, toDate, groupProduct, downlineId);
        }

        if (custLevel.IsMaster())
        {
            return await GetSumWinLossAgent(custId, custLevel, fromDate, toDate, groupProduct, downlineId);
        }

        return await GetSumWinLossMember(custId, custLevel, fromDate, toDate, groupProduct, downlineId);
    }

    public async Task<IEnumerable<string>> GetTopUsername(int custId, UserLevel custLevel, DateTime fromDate, DateTime toDate, string groupProduct, int top)
    {
        if (custLevel.IsSuper())
        {
            return (await GetWinLoss<WinLossMaster>(custId, custLevel, fromDate, toDate, groupProduct, null))
                .OrderByDescending(w => Math.Abs(w.MasterTotal))
                .Select(w => w.CustName)
                .Take(top);
        }

        if (custLevel.IsMaster())
        {
            return (await GetWinLoss<WinLossAgent>(custId, custLevel, fromDate, toDate, groupProduct, null))
                .OrderByDescending(w => Math.Abs(w.AgentTotal))
                .Select(w => w.CustName)
                .Take(top);
        }

        return (await GetWinLoss<WinLossMember>(custId, custLevel, fromDate, toDate, groupProduct, null))
                .OrderByDescending(w => Math.Abs(w.MemberWinLoss))
                .Select(w => w.CustName)
                .Take(top);
    }

    private async Task<WinLossMaster> GetSumWinLossMaster(int custId, UserLevel custLevel, DateTime fromDate, DateTime toDate, string groupProduct, int? downlineId)
    {
        var winloss = await GetWinLoss<WinLossMaster>(custId, custLevel, fromDate, toDate, groupProduct, downlineId);

        if (!winloss.Any())
        {
            return null;
        }

        var sumWinLossMaster = new WinLossMaster();

        foreach (var item in winloss)
        {
            sumWinLossMaster += item;
        }

        return sumWinLossMaster;
    }

    private async Task<WinLossAgent> GetSumWinLossAgent(int custId, UserLevel custLevel, DateTime fromDate, DateTime toDate, string groupProduct, int? downlineId)
    {
        var winloss = await GetWinLoss<WinLossAgent>(custId, custLevel, fromDate, toDate, groupProduct, downlineId);

        if (!winloss.Any())
        {
            return null;
        }

        var sumWinLossAgent = new WinLossAgent();

        foreach (var item in winloss)
        {
            sumWinLossAgent += item;
        }

        return sumWinLossAgent;
    }

    private async Task<WinLossMember> GetSumWinLossMember(int custId, UserLevel custLevel, DateTime fromDate, DateTime toDate, string groupProduct, int? downlineId)
    {
        var winloss = await GetWinLoss<WinLossMember>(custId, custLevel, fromDate, toDate, groupProduct, downlineId);

        if (!winloss.Any())
        {
            return null;
        }

        var sumWinLossMember = new WinLossMember();

        foreach (var item in winloss)
        {
            sumWinLossMember += item;
        }

        return sumWinLossMember;
    }

    private async Task<IEnumerable<T>> GetWinLoss<T>(int custId, UserLevel custLevel, DateTime fromDate, DateTime toDate, string groupProduct, int? downlineId)
    {
        var limitDate = await reportDateService.GetLimitDate(custId);
        fromDate = limitDate > fromDate ? limitDate : fromDate;

        var productIdsTask = productService.GetOfferedProducts(
            custId,
            appSettings.SettingStatus,
            SystemTime.Today.AddMonths(-3).FirstDayOfMonth());
        var reportDateTask = reportDateService.GetBalanceUpDayAndMoveBetsDay();
        await Task.WhenAll(productIdsTask, reportDateTask);

        var productIds = await productIdsTask;
        if (groupProduct.Equals(GroupProductConst.Sportsbook, StringComparison.OrdinalIgnoreCase))
        {
            productIds = productIds.Where(id => id == SportsbookProductId);
        }
        else if (!groupProduct.Equals(GroupProductConst.All, StringComparison.OrdinalIgnoreCase))
        {
            productIds = productIds.Where(id => id != SportsbookProductId);
        }

        var reportDate = await reportDateTask;
        CriteriaBase criteria = custLevel.IsAgent() ?
            new GetMemberWinLossCriteria(
                custId,
                fromDate,
                toDate,
                productIds.JoinWithComma(),
                reportDate.MoveBetsDay,
                reportDate.BalanceUpDay,
                custLevel) :
            new GetWinLossCriteria(
                custId,
                fromDate,
                toDate,
                productIds.JoinWithComma(),
                reportDate.MoveBetsDay,
                reportDate.BalanceUpDay,
                custLevel,
                downlineId);

        return await dynamicRepository.FetchAsync<T>(criteria);
    }

    public async Task<EnteredResult> GetYesterdayEnteredSportResult()
    {
        var reportData = await cacheService.GetOrSetAsync(
            EnteredResultsCacheKey,
            GetYesterdayEnteredSport,
            new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(1)));

        var totalEntered = 0;
        var totalNonEntered = 0;

        if (reportData.Any())
        {
            foreach (var data in reportData)
            {
                totalEntered += data.Entered;
                totalNonEntered += data.NonEntered;
            }
        }

        return new EnteredResult(totalEntered, totalNonEntered);
    }

    private async Task<IEnumerable<DailyEnteredSport>> GetYesterdayEnteredSport()
    {
        var criteria = new DailyEnteredSportCriteria(appSettings.LocalTest);
        var dailyEnteredSportData = await dynamicRepository.FetchAsync<DailyEnteredSport>(criteria);

        if (dailyEnteredSportData.Any())
        {
            return dailyEnteredSportData.Where(x => !x.IsToday);
        }

        return Enumerable.Empty<DailyEnteredSport>();
    }
}
