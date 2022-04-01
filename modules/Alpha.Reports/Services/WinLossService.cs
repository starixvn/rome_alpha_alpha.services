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
    Task<WinLossBase> GetWinLossAsync(int custId, UserLevel custLevel, DateTime date);
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
        IOptions<ReportAppSettings> options,
        IDynamicRepository dynamicRepository)
    {
        this.productService = productService;
        this.reportDateService = reportDateService;
        appSettings = options.Value;
        this.dynamicRepository = dynamicRepository;
    }

    public async Task<WinLossBase> GetWinLossAsync(int custId, UserLevel custLevel, DateTime date)
    {
        if (custLevel.IsSuper())
        {
            return await GetAsync<WinLossSuper>(custId, custLevel, date);
        }

        if (custLevel.IsMaster())
        {
            return await GetAsync<WinLossMaster>(custId, custLevel, date);
        }

        return await GetAsync<WinLossAgent>(custId, custLevel, date);
    }

    private async Task<T> GetAsync<T>(int custId, UserLevel custLevel, DateTime date)
    {
        var productIds = await productService.GetOfferedProducts(
            custId,
            appSettings.SettingStatus,
            SystemTime.Today.AddMonths(-3).FirstDayOfMonth());
        var reportDate = await reportDateService.GetBalanceUpDayAndMoveBetsDayAsync();

        return await dynamicRepository.GetAsync<T>(
            new GetWinLossCriteria(
                custId,
                date,
                date,
                productIds.JoinWithComma(),
                reportDate.MoveBetsDay,
                reportDate.BalanceUpDay,
                custLevel));
    }
}
