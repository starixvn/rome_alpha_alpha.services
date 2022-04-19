using Alpha.Reports.Criteria.BalanceInfo;
using Alpha.Reports.Models.BalanceInfo;
using Alpha.Reports.Models.Outstanding;
using Alpha.Shared.Enumerations;
using Fanex.Data.Repository;

namespace Alpha.Reports.Services;

public interface IBalanceInfoService
{
    Task<CustomerBalance> GetCustomerBalance(int custId, UserLevel custLevel);

    Task<DownlineBalance> GetDownlineBalance(int custId, UserLevel custLevel, int downlineId);

    Task<IEnumerable<string>> GetTopUsername(int custId, UserLevel custLevel, int top);

    Task<IEnumerable<TopBalance>> GetTopBalance(int custId, UserLevel custLevel, int top);
}

public class BalanceInfoService : IBalanceInfoService
{
    private readonly IReportDateService reportDateService;
    private readonly IOutstandingService outstandingService;
    private readonly IDynamicRepository dynamicRepository;

    public BalanceInfoService(
        IReportDateService reportDateService,
        IOutstandingService outstandingService,
        IDynamicRepository dynamicRepository)
    {
        this.reportDateService = reportDateService;
        this.outstandingService = outstandingService;
        this.dynamicRepository = dynamicRepository;
    }

    public async Task<CustomerBalance> GetCustomerBalance(int custId, UserLevel custLevel)
    {
        var moveBetsDay = await reportDateService.GetMoveBetsDay();

        var statisticsTask = outstandingService.GetStatistics(custId, custLevel);
        var balanceTask = dynamicRepository.GetAsync<CustomerBalance>(
            new GetCustomerBalanceCriteria(custId, custLevel, moveBetsDay));

        await Task.WhenAll(statisticsTask, balanceTask);

        var statistics = await statisticsTask;
        var balance = await balanceTask;
        balance.Outstanding = statistics.Outstanding;
        balance.TotalOutstanding = statistics.TotalOutstanding;

        return balance;
    }

    public Task<DownlineBalance> GetDownlineBalance(int custId, UserLevel custLevel, int downlineId)
    {
        if (custLevel.IsSuper())
        {
            return GetMasterBalance(custId, downlineId);
        }

        if (custLevel.IsMaster())
        {
            return GetAgentBalance(custId, downlineId);
        }

        return GetMemberBalance(custId, downlineId);
    }

    private async Task<DownlineBalance> GetMasterBalance(int custId, int downlineId)
    {
        var balanceTask = dynamicRepository.GetAsync<MasterBalance>(
            new GetDownlineBalanceCriteria(downlineId, UserLevel.Master));

        var creditInfoTask = GetCreditInfo(downlineId);

        var statisticsTask = outstandingService.GetStatistics(custId, UserLevel.Super);

        var totalDownlinesTask = GetTotalDownlines(downlineId, UserLevel.Master);

        await Task.WhenAll(balanceTask, creditInfoTask, statisticsTask, totalDownlinesTask);

        return new DownlineBalance(await balanceTask, await creditInfoTask, await statisticsTask, await totalDownlinesTask);
    }

    private async Task<DownlineBalance> GetAgentBalance(int custId, int downlineId)
    {
        var balanceTask = dynamicRepository.GetAsync<AgentBalance>(
            new GetDownlineBalanceCriteria(downlineId, UserLevel.Agent));

        var creditInfoTask = GetCreditInfo(downlineId);

        var statisticsTask = outstandingService.GetStatistics(custId, UserLevel.Master);

        var totalDownlinesTask = GetTotalDownlines(downlineId, UserLevel.Agent);

        await Task.WhenAll(balanceTask, creditInfoTask, statisticsTask, totalDownlinesTask);

        return new DownlineBalance(await balanceTask, await creditInfoTask, await statisticsTask, await totalDownlinesTask);
    }

    private async Task<DownlineBalance> GetMemberBalance(int custId, int downlineId)
    {
        var outstandingCommand = new GetMemberOutstandingCommand(downlineId);
        var outstandingTask = dynamicRepository.ExecuteAsync(outstandingCommand);

        var balanceInfoCommand = new GetMemberBalanceCommand(downlineId);
        var balanceInfoTask = dynamicRepository.ExecuteAsync(balanceInfoCommand);

        var balanceTask = dynamicRepository.GetAsync<MemberBalance>(
            new GetDownlineBalanceCriteria(downlineId, UserLevel.Member));

        var creditInfoTask = GetCreditInfo(downlineId);

        var statisticsTask = outstandingService.GetStatistics(custId, UserLevel.Agent);

        await Task.WhenAll(outstandingTask, balanceInfoTask, balanceTask, creditInfoTask, statisticsTask);

        var memberBalance = await balanceTask;
        memberBalance.Balance = balanceInfoCommand.Balance;
        memberBalance.NetPosition = balanceInfoCommand.NetPosition;
        memberBalance.PendingTransfer3rd = balanceInfoCommand.PendingTransfer3rd;
        memberBalance.CustomerOutstanding = outstandingCommand.Outstanding;

        return new DownlineBalance(memberBalance, await creditInfoTask, await statisticsTask);
    }

    private Task<CreditInfo> GetCreditInfo(int custId)
        => dynamicRepository.GetAsync<CreditInfo>(new GetCreditInfoCriteria(custId));

    private Task<IEnumerable<TotalDownline>> GetTotalDownlines(int custId, UserLevel custLevel)
        => dynamicRepository.FetchAsync<TotalDownline>(new GetTotalDownlinesCriteria(custId, custLevel.Value));

    public async Task<IEnumerable<string>> GetTopUsername(int custId, UserLevel custLevel, int top)
        => (await GetTopBalance(custId, custLevel, top)).Select(b => b.Username);

    public Task<IEnumerable<TopBalance>> GetTopBalance(int custId, UserLevel custLevel, int top)
        => dynamicRepository.FetchAsync<TopBalance>(new GetTopBalanceCriteria(custId, custLevel, top));
}
