using Alpha.Reports.Criteria.BalanceInfo;
using Alpha.Reports.Models.BalanceInfo;
using Alpha.Shared.Enumerations;
using Fanex.Data.Repository;

namespace Alpha.Reports.Services;

public interface IBalanceInfoService
{
    Task<MyBalance> GetMyBalanceAsync(int custId, UserLevel custLevel);

    Task<IEnumerable<string>> GetTopUsernameAsync(int custId, UserLevel custLevel, int top);

    Task<TopBalanceReport> GetTopBalanceReportAsync(int custId, UserLevel custLevel, int top);
}

public class BalanceInfoService : IBalanceInfoService
{
    private readonly IReportDateService reportDateService;
    private readonly IDynamicRepository dynamicRepository;

    public BalanceInfoService(IReportDateService reportDateService, IDynamicRepository dynamicRepository)
    {
        this.reportDateService = reportDateService;
        this.dynamicRepository = dynamicRepository;
    }
    
    public async Task<MyBalance> GetMyBalanceAsync(int custId, UserLevel custLevel)
    {
        var moveBetsDay = await reportDateService.GetMoveBetsDayAsync();

        return await dynamicRepository.GetAsync<MyBalance>(
            new GetMyBalanceCriteria(custId, custLevel, moveBetsDay));
    }

    public async Task<IEnumerable<string>> GetTopUsernameAsync(int custId, UserLevel custLevel, int top)
    {
        var balances = await GetTopBalanceAsync(custId, custLevel);

        return balances.Take(top).Select(o => o.Username);
    }

    public async Task<TopBalanceReport> GetTopBalanceReportAsync(int custId, UserLevel custLevel, int top)
    {
        var balances = await GetTopBalanceAsync(custId, custLevel);
        var totalBalances = balances.Count();

        if (totalBalances == 0)
        {
            return new TopBalanceReport();
        }

        var hiddenDownlinesCount = totalBalances - top;

        if (top > totalBalances)
        {
            top = totalBalances;
            hiddenDownlinesCount = 0;
        }
        
        var (topBalances, total) = GetTopAndAccummulateTotal(balances, top);

        return new TopBalanceReport(topBalances, total, hiddenDownlinesCount);
    }

    private async Task<IEnumerable<TopBalance>> GetTopBalanceAsync(int custId, UserLevel custLevel)
    {
        var balances = await dynamicRepository.FetchAsync<TopBalance>(new GetTopBalanceCriteria(custId, custLevel));
        
        return balances.OrderByDescending(o => o.Balance);
    }

    private static Tuple<IEnumerable<TopBalance>, TopBalance> GetTopAndAccummulateTotal(IEnumerable<TopBalance> balances, int top)
    {
        var index = 0;
        var topBalances = new List<TopBalance>();
        var totalBalance = 0M;
        var totalYesterdayBalance = 0M;

        balances
            .ToList()
            .ForEach(item =>
            {
                if (index < top)
                {
                    topBalances.Add(item);
                    index++;
                }

                totalBalance += item.Balance;
                totalYesterdayBalance += item.YesterdayBalance;
            });

        return new Tuple<IEnumerable<TopBalance>, TopBalance>(
            topBalances,
            new TopBalance("Total", totalBalance, totalYesterdayBalance));
    }
}
