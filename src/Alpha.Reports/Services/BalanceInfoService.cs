using Alpha.Customers.Services;
using Alpha.Reports.Criteria.BalanceInfo;
using Alpha.Reports.Models.BalanceInfo;
using Alpha.Shared.Enumerations;
using Fanex.Data.Repository;

namespace Alpha.Reports.Services;

public interface IBalanceInfoService
{
    Task<CustomerBalance> GetCustomerBalance(int custId, UserLevel custLevel);

    Task<DownlineBalance> GetDownlineBalance(int custId, UserLevel custLevel);

    Task<IEnumerable<string>> GetTopUsername(int custId, UserLevel custLevel, int top);

    Task<IEnumerable<TopBalance>> GetTopBalance(int custId, UserLevel custLevel, int top);
}

public class BalanceInfoService : IBalanceInfoService
{
    private readonly ICustomerService customerService;
    private readonly IOutstandingService outstandingService;
    private readonly IDynamicRepository dynamicRepository;

    public BalanceInfoService(
        ICustomerService customerService,
        IReportDateService reportDateService,
        IOutstandingService outstandingService,
        IDynamicRepository dynamicRepository)
    {
        this.customerService = customerService;     
        this.outstandingService = outstandingService;
        this.dynamicRepository = dynamicRepository;
    }

    public async Task<CustomerBalance> GetCustomerBalance(int custId, UserLevel custLevel)
    {
        var outstandingTask = outstandingService.GetCustomerOutstanding(custId);
        var balanceTask = dynamicRepository.GetAsync<CustomerBalance>(
            new GetCustomerBalanceCriteria(custId, custLevel));

        await Task.WhenAll(outstandingTask, balanceTask);

        var balance = await balanceTask;
        balance.Outstanding = await outstandingTask;

        return balance;
    }

    public Task<DownlineBalance> GetDownlineBalance(int custId, UserLevel custLevel)
    {
        if (custLevel.IsMaster())
        {
            return GetMasterBalance(custId);
        }

        if (custLevel.IsAgent())
        {
            return GetAgentBalance(custId);
        }

        return GetMemberBalance(custId);
    }

    private async Task<DownlineBalance> GetMasterBalance(int custId)
    {
        var balanceTask = dynamicRepository.GetAsync<MasterBalance>(
            new GetDownlineBalanceCriteria(custId, UserLevel.Master));

        var creditInfoTask = GetCreditInfo(custId);

        var outstandingTask = outstandingService.GetCustomerOutstanding(custId);

        var totalDownlinesTask = GetTotalDownlines(custId, UserLevel.Master);

        await Task.WhenAll(balanceTask, creditInfoTask, outstandingTask, totalDownlinesTask);

        return new DownlineBalance(await balanceTask, await creditInfoTask, await totalDownlinesTask, await outstandingTask);
    }

    private async Task<DownlineBalance> GetAgentBalance(int custId)
    {
        var balanceTask = dynamicRepository.GetAsync<AgentBalance>(
            new GetDownlineBalanceCriteria(custId, UserLevel.Agent));

        var creditInfoTask = GetCreditInfo(custId);

        var outstandingTask = outstandingService.GetCustomerOutstanding(custId);

        var totalDownlinesTask = GetTotalDownlines(custId, UserLevel.Agent);

        await Task.WhenAll(balanceTask, creditInfoTask, outstandingTask, totalDownlinesTask);

        return new DownlineBalance(await balanceTask, await creditInfoTask, await totalDownlinesTask, await outstandingTask);
    }

    private async Task<DownlineBalance> GetMemberBalance(int custId)
    {
        var siteDBGroup = await customerService.GetSiteDBGroup(custId);
        var balanceInfoCommand = new GetMemberBalanceCommand(custId, siteDBGroup.SiteDBGroupID);
        var balanceInfoTask = dynamicRepository.ExecuteAsync(balanceInfoCommand);

        var balanceTask = dynamicRepository.GetAsync<MemberBalance>(
            new GetDownlineBalanceCriteria(custId, UserLevel.Member));

        var creditInfoTask = GetCreditInfo(custId);

        var outstandingTask = outstandingService.GetCustomerOutstanding(custId);

        await Task.WhenAll(balanceInfoTask, balanceTask, creditInfoTask, outstandingTask);

        var outstanding = await outstandingTask;
        var memberBalance = await balanceTask;
        memberBalance.Balance = balanceInfoCommand.Balance;
        memberBalance.NetPosition = balanceInfoCommand.NetPosition;
        memberBalance.PendingTransfer3rd = balanceInfoCommand.PendingTransfer3rd;
        memberBalance.CustomerOutstanding = outstanding.MemberOutstanding;

        return new DownlineBalance(memberBalance, await creditInfoTask, outstanding);
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
