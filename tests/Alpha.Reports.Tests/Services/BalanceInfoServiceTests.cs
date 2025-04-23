using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alpha.Customers.Services;
using Alpha.Reports.Criteria.BalanceInfo;
using Alpha.Reports.Models.BalanceInfo;
using Alpha.Reports.Models.Outstanding;
using Alpha.Reports.Services;
using Alpha.Shared.Enumerations;
using Alpha.Shared.Extensions;
using Fanex.Data.Repository;
using NSubstitute;
using Xunit;

namespace Alpha.Reports.Tests.Services;

public class BalanceInfoServiceTests
{
    private readonly ICustomerService customerService;
    private readonly IReportDateService reportDateService;
    private readonly IOutstandingService outstandingService;
    private readonly IDynamicRepository dynamicRepository;
    private readonly IBalanceInfoService balanceInfoService;

    public BalanceInfoServiceTests()
    {
        customerService = Substitute.For<ICustomerService>();
        reportDateService = Substitute.For<IReportDateService>();
        outstandingService = Substitute.For<IOutstandingService>();
        dynamicRepository = Substitute.For<IDynamicRepository>();
        balanceInfoService = new BalanceInfoService(customerService, reportDateService, outstandingService, dynamicRepository);
    }

    [Fact]
    public async Task GetCustomerBalance_Always_ReturnsCorrectCustomerBalance()
    {
        // Arrange
        var custId = 1176070;
        var custLevel = UserLevel.Super;

        var moveBetsDay = DateTime.Today;
        reportDateService.GetMoveBetsDay().Returns(Task.FromResult(moveBetsDay));

        var outstanding = new CustomerOutstanding(12, 34, 56, 78);
        outstandingService.GetCustomerOutstanding(custId).Returns(Task.FromResult(outstanding));

        var balance = new CustomerBalance(
            "UUS",
            -39.09M,
            -39.09M,
            4,
            4,
            21.19M,
            21.19M,
            1_000_000_005,
            100_099_122,
            300,
            outstanding,
            0,
            0,
            "",
            null
        );
        dynamicRepository.GetAsync<CustomerBalance>(Arg.Any<GetCustomerBalanceCriteria>()).Returns(Task.FromResult(balance));

        // Act
        var actual = await balanceInfoService.GetCustomerBalance(custId, custLevel);

        // Assert
        Assert.Equal(balance, actual);
    }

    [Fact]
    public async Task GetDownlineBalance_Master_ReturnsCorrectBalance()
    {
        // Arrange
        var custId = 1176070;
        var custLevel = UserLevel.Master;

        var balance = new MasterBalance(
            100,
            100,
            200,
            300,
            300,
            400,
            400,
            500,
            600,
            0,
            0,
            ""
        );
        dynamicRepository.GetAsync<MasterBalance>(Arg.Any<GetDownlineBalanceCriteria>()).Returns(Task.FromResult(balance));

        var credit = new CreditInfo(1000, 2000, 3000);
        dynamicRepository.GetAsync<CreditInfo>(Arg.Any<GetCreditInfoCriteria>()).Returns(Task.FromResult(credit));

        var outstanding = new CustomerOutstanding(700, 750, 800, 850);
        outstandingService.GetCustomerOutstanding(custId).Returns(Task.FromResult(outstanding));

        var totalDownlines = new List<TotalDownline>
        {
            new TotalDownline {
                RoleId=2,
                TotalActive=10,
                TotalClosed=5,
                TotalSuspended=1
            },
            new TotalDownline {
                RoleId=1,
                TotalActive=10,
                TotalClosed=4,
                TotalSuspended=0
            }
        };
        dynamicRepository
            .FetchAsync<TotalDownline>(Arg.Any<GetTotalDownlinesCriteria>())
            .Returns(Task.FromResult<IEnumerable<TotalDownline>>(totalDownlines));

        // Act
        var actual = await balanceInfoService.GetDownlineBalance(custId, custLevel);
        var actualTotalDownlines = actual.TotalDownlines.ToList();

        // Assert
        Assert.True(balance.MasterCash == actual.TotalCash
            && balance.YesterdayMasterCash == actual.YesterdayCash
            && balance.MasterCash + balance.AgentCash + balance.CustomerCash == actual.TotalBalance
            && balance.YesterdayMasterCash + balance.YesterdayAgentCash + balance.YesterdayCustomerCash == actual.YesterdayBalance
            && balance.PendingTransfer3rd == actual.PendingTransfer3rd
            && credit.UsedCredit == actual.UsedCredit
            && credit.GivenCredit == actual.GivenCredit);
        Assert.True(totalDownlines[0].RoleId == actualTotalDownlines[0].RoleId
            && totalDownlines[0].TotalActive == actualTotalDownlines[0].TotalActive
            && totalDownlines[0].TotalClosed == actualTotalDownlines[0].TotalClosed
            && totalDownlines[0].TotalSuspended == actualTotalDownlines[0].TotalSuspended
            && totalDownlines[1].RoleId == actualTotalDownlines[1].RoleId
            && totalDownlines[1].TotalActive == actualTotalDownlines[1].TotalActive
            && totalDownlines[1].TotalClosed == actualTotalDownlines[1].TotalClosed
            && totalDownlines[1].TotalSuspended == actualTotalDownlines[1].TotalSuspended);
        Assert.True(outstanding.SuperOutstanding == actual.Outstanding.SuperOutstanding
            && outstanding.MasterOutstanding == actual.Outstanding.MasterOutstanding
            && outstanding.AgentOutstanding == actual.Outstanding.AgentOutstanding
            && outstanding.MemberOutstanding == actual.Outstanding.MemberOutstanding);
    }

    [Fact]
    public async Task GetDownlineBalance_Agent_ReturnsCorrectBalance()
    {
        // Arrange
        var custId = 1176070;
        var custLevel = UserLevel.Agent;

        var balance = new AgentBalance(
            100,
            100,
            200,
            300,
            300,
            400,
            500,
            0,
            0,
            ""
        );
        dynamicRepository.GetAsync<AgentBalance>(Arg.Any<GetDownlineBalanceCriteria>()).Returns(Task.FromResult(balance));

        var credit = new CreditInfo(1000, 2000, 3000);
        dynamicRepository.GetAsync<CreditInfo>(Arg.Any<GetCreditInfoCriteria>()).Returns(Task.FromResult(credit));

        var outstanding = new CustomerOutstanding(700, 750, 800, 850);
        outstandingService.GetCustomerOutstanding(custId).Returns(Task.FromResult(outstanding));

        var totalDownlines = new List<TotalDownline>
        {
            new TotalDownline {
                RoleId=1,
                TotalActive=10,
                TotalClosed=4,
                TotalSuspended=0
            }
        };
        dynamicRepository
            .FetchAsync<TotalDownline>(Arg.Any<GetTotalDownlinesCriteria>())
            .Returns(Task.FromResult<IEnumerable<TotalDownline>>(totalDownlines));

        // Act
        var actual = await balanceInfoService.GetDownlineBalance(custId, custLevel);
        var actualTotalDownlines = actual.TotalDownlines.ToList();

        // Assert
        Assert.True(balance.AgentCash == actual.TotalCash
            && balance.YesterdayAgentCash == actual.YesterdayCash
            && balance.AgentCash + balance.CustomerCash == actual.TotalBalance
            && balance.YesterdayAgentCash + balance.YesterdayCustomerCash == actual.YesterdayBalance
            && balance.PendingTransfer3rd == actual.PendingTransfer3rd
            && credit.UsedCredit == actual.UsedCredit
            && credit.GivenCredit == actual.GivenCredit
            && credit.MemberMaxCredit == actual.MemberMaxCredit);
        Assert.True(totalDownlines[0].RoleId == actualTotalDownlines[0].RoleId
            && totalDownlines[0].TotalActive == actualTotalDownlines[0].TotalActive
            && totalDownlines[0].TotalClosed == actualTotalDownlines[0].TotalClosed
            && totalDownlines[0].TotalSuspended == actualTotalDownlines[0].TotalSuspended);
        Assert.True(outstanding.SuperOutstanding == actual.Outstanding.SuperOutstanding
            && outstanding.MasterOutstanding == actual.Outstanding.MasterOutstanding
            && outstanding.AgentOutstanding == actual.Outstanding.AgentOutstanding
            && outstanding.MemberOutstanding == actual.Outstanding.MemberOutstanding);
    }

    [Fact]
    public async Task GetDownlineBalance_Member_ReturnsCorrectBalance()
    {
        // Arrange
        var custId = 1176070;
        var custLevel = UserLevel.Member;

        dynamicRepository.ExecuteAsync(Arg.Any<GetMemberBalanceCommand>()).Returns(Task.FromResult(0));

        var balance = new MemberBalance(100, 0, "");
        dynamicRepository.GetAsync<MemberBalance>(Arg.Any<GetDownlineBalanceCriteria>()).Returns(Task.FromResult(balance));

        var credit = new CreditInfo(1000, 2000, 3000);
        dynamicRepository.GetAsync<CreditInfo>(Arg.Any<GetCreditInfoCriteria>()).Returns(Task.FromResult(credit));

        var outstanding = new CustomerOutstanding(700, 750, 800, 850);
        outstandingService.GetCustomerOutstanding(custId).Returns(Task.FromResult(outstanding));

        // Act
        var actual = await balanceInfoService.GetDownlineBalance(custId, custLevel);

        // Assert
        Assert.True(outstanding.MemberOutstanding == actual.TotalBalance
            && balance.YesterdayCustomerCash == actual.YesterdayBalance
            && 0 == actual.PendingTransfer3rd
            && credit.GivenCredit == actual.GivenCredit
            && 0 == actual.NetPosition
            && credit.GivenCredit == actual.BetCredit);
        Assert.Null(actual.TotalDownlines);
        Assert.True(outstanding.SuperOutstanding == actual.Outstanding.SuperOutstanding
            && outstanding.MasterOutstanding == actual.Outstanding.MasterOutstanding
            && outstanding.AgentOutstanding == actual.Outstanding.AgentOutstanding
            && outstanding.MemberOutstanding == actual.Outstanding.MemberOutstanding);
    }

    [Fact]
    public async Task GetTopUsername_Always_ReturnsCorrectUsernames()
    {
        // Arrange
        var custId = 1176070;
        var custLevel = UserLevel.Super;
        var top = 10;
        var balances = new List<TopBalance>
        {
            new("master1", 10, 10),
            new("master2", 20, 20),
            new("master3", 30, 30),
            new("master4", 40, 40),
            new("master5", 50, 50)
        };
        dynamicRepository
            .FetchAsync<TopBalance>(Arg.Any<GetTopBalanceCriteria>())
            .Returns(Task.FromResult<IEnumerable<TopBalance>>(balances));

        // Act
        var actual = await balanceInfoService.GetTopUsername(custId, custLevel, top);

        // Assert
        Assert.Equal("master1,master2,master3,master4,master5", actual.JoinWithComma());
    }

    [Fact]
    public async Task GetTopBalance_Always_CallsFetchAsync()
    {
        // Arrange
        var custId = 1176070;
        var custLevel = UserLevel.Super;
        var top = 10;

        // Act
        await balanceInfoService.GetTopUsername(custId, custLevel, top);

        // Assert
        await dynamicRepository.Received().FetchAsync<TopBalance>(Arg.Is<GetTopBalanceCriteria>(
            criteria => custId == criteria.CustId
            && custLevel == criteria.CustLevel
            && top == criteria.PageSize
        ));
    }
}
