using System.Threading.Tasks;
using Alpha.API.Handlers;
using Alpha.API.Requests.Reports.BalanceInfo;
using Alpha.Reports.Services;
using Alpha.Shared.Enumerations;
using NSubstitute;
using Xunit;

namespace Alpha.API.Tests.Handlers;

public class BalanceInfoHandlerTests
{
    private readonly IBalanceInfoService balanceInfoService;
    private readonly BalanceInfoHandler balanceInfoHandler;

    public BalanceInfoHandlerTests()
    {
        balanceInfoService = Substitute.For<IBalanceInfoService>();
        balanceInfoHandler = new BalanceInfoHandler(balanceInfoService);
    }

    [Fact]
    public async Task Handle_GetCustomerBalanceQuery_CallsGetCustomerBalance()
    {
        // Arrange
        var query = new GetCustomerBalanceQuery(1176070, UserLevel.Super.Value);

        // Act
        await balanceInfoHandler.Handle(query, default);

        // Assert
        await balanceInfoService.Received().GetCustomerBalance(
            Arg.Is<int>(custId => 1176070 == custId),
            Arg.Is<UserLevel>(custLevel => UserLevel.Super == custLevel)
        );
    }

    [Fact]
    public async Task Handle_GetDownlineBalanceQuery_CallsGetDownlineBalance()
    {
        // Arrange
        var query = new GetDownlineBalanceQuery(1176070, UserLevel.Super.Value);

        // Act
        await balanceInfoHandler.Handle(query, default);

        // Assert
        await balanceInfoService.Received().GetDownlineBalance(
            Arg.Is<int>(custId => 1176070 == custId),
            Arg.Is<UserLevel>(custLevel => UserLevel.Super == custLevel)
        );
    }

    [Fact]
    public async Task Handle_GetTopBalanceUsernameQuery_CallsGetTopUsername()
    {
        // Arrange
        var query = new GetTopBalanceUsernameQuery(1176070, UserLevel.Super.Value, 10);

        // Act
        await balanceInfoHandler.Handle(query, default);

        // Assert
        await balanceInfoService.Received().GetTopUsername(
            Arg.Is<int>(custId => 1176070 == custId),
            Arg.Is<UserLevel>(custLevel => UserLevel.Super == custLevel),
            Arg.Is<int>(top => 10 == top)
        );
    }

    [Fact]
    public async Task Handle_GetTopBalanceQuery_CallsGetTopBalance()
    {
        // Arrange
        var query = new GetTopBalanceQuery(1176070, UserLevel.Super.Value, 10);

        // Act
        await balanceInfoHandler.Handle(query, default);

        // Assert
        await balanceInfoService.Received().GetTopBalance(
            Arg.Is<int>(custId => 1176070 == custId),
            Arg.Is<UserLevel>(custLevel => UserLevel.Super == custLevel),
            Arg.Is<int>(top => 10 == top)
        );
    }
}
