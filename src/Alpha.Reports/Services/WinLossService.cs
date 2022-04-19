using Alpha.Reports.Configurations;
using Alpha.Reports.Criteria.WinLoss;
using Alpha.Reports.Models.WinLoss;
using Alpha.Shared.Enumerations;
using Alpha.Shared.Extensions;
using Alpha.Shared.Utils;
using Fanex.Data.Repository;
using Microsoft.Extensions.Options;

namespace Alpha.Reports.Services;

public interface IWinLossService
{
    Task<WinLossBase> GetWinLoss(int custId, UserLevel custLevel, DateTime date);

    Task<WinLossBase> GetDownlineWinLoss(int custId, UserLevel custLevel, DateTime date, int downlineId);

    Task<IEnumerable<string>> GetTopUsername(int custId, UserLevel custLevel, DateTime date, int top);
}

public class WinLossService : IWinLossService
{
    private readonly IProductService productService;
    private readonly IReportDateService reportDateService;
    private readonly ReportAppSettings appSettings;
    private readonly IDynamicRepository dynamicRepository;

    public WinLossService(
        IProductService productService,
        IReportDateService reportDateService,
        IMinimizeUMService minimizeUMService,
        IOptions<ReportAppSettings> options,
        IDynamicRepository dynamicRepository)
    {
        this.productService = productService;
        this.reportDateService = reportDateService;
        appSettings = options.Value;
        this.dynamicRepository = dynamicRepository;
    }

    public async Task<WinLossBase> GetWinLoss(int custId, UserLevel custLevel, DateTime date)
    {
        if (custLevel.IsSuper())
        {
            return await GetSumWinLossMaster(custId, custLevel, date);
        }

        if (custLevel.IsMaster())
        {
            return await GetSumWinLossAgent(custId, custLevel, date);
        }

        return await GetSumWinLossMember(custId, custLevel, date);
    }

    public async Task<WinLossBase> GetDownlineWinLoss(int custId, UserLevel custLevel, DateTime date, int downlineId)
    {
        var directDownline = custLevel.GetDirectDownlineLevel();

        if (directDownline.IsMaster())
        {
            return await GetSumWinLossAgent(downlineId, directDownline, date);
        }

        if (directDownline.IsAgent())
        {
            return await GetSumWinLossMember(downlineId, directDownline, date);
        }

        return (await GetWinLoss<WinLossMember>(custId, custLevel, date)).FirstOrDefault(w => w.CustId == downlineId);
    }

    public async Task<IEnumerable<string>> GetTopUsername(int custId, UserLevel custLevel, DateTime date, int top)
    {
        if (custLevel.IsSuper())
        {
            return (await GetWinLoss<WinLossMaster>(custId, custLevel, date))
                .OrderByDescending(w => w.MasterTotal)
                .Select(w => w.CustName)
                .Take(top);
        }

        if (custLevel.IsMaster())
        {
            return (await GetWinLoss<WinLossAgent>(custId, custLevel, date))
                .OrderByDescending(w => w.AgentTotal)
                .Select(w => w.CustName)
                .Take(top);
        }

        return (await GetWinLoss<WinLossMember>(custId, custLevel, date))
                .OrderByDescending(w => w.MemberWinLoss)
                .Select(w => w.CustName)
                .Take(top);
    }

    private async Task<WinLossMaster> GetSumWinLossMaster(int custId, UserLevel custLevel, DateTime date)
    {
        var winloss = await GetWinLoss<WinLossMaster>(custId, custLevel, date);

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

    private async Task<WinLossAgent> GetSumWinLossAgent(int custId, UserLevel custLevel, DateTime date)
    {
        var winloss = await GetWinLoss<WinLossAgent>(custId, custLevel, date);

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

    private async Task<WinLossMember> GetSumWinLossMember(int custId, UserLevel custLevel, DateTime date)
    {
        var winloss = await GetWinLoss<WinLossMember>(custId, custLevel, date);

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
    
    private async Task<IEnumerable<T>> GetWinLoss<T>(int custId, UserLevel custLevel, DateTime date)
    {
        var productIdsTask = productService.GetOfferedProducts(
            custId,
            appSettings.SettingStatus,
            SystemTime.Today.AddMonths(-3).FirstDayOfMonth());
        var reportDateTask = reportDateService.GetBalanceUpDayAndMoveBetsDay();

        await Task.WhenAll(productIdsTask, reportDateTask);

        var reportDate = await reportDateTask;
        
        return await dynamicRepository.FetchAsync<T>(new GetWinLossCriteria(
            custId,
            date,
            date,
            (await productIdsTask).JoinWithComma(),
            reportDate.MoveBetsDay,
            reportDate.BalanceUpDay,
            custLevel));
    }
}
