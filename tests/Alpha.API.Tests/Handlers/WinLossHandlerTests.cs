using System;
using System.Threading.Tasks;
using Alpha.API.Handlers;
using Alpha.API.Requests.Reports.WinLoss;
using Alpha.Reports.Services;
using Alpha.Shared.Enumerations;
using NSubstitute;
using Xunit;

namespace Alpha.API.Tests.Handlers;

public class WinLossHandlerTests
{
    private readonly IWinLossService winLossService;
    private readonly WinLossHandler winLossHandler;

    public WinLossHandlerTests()
    {
        winLossService = Substitute.For<IWinLossService>();
        winLossHandler = new WinLossHandler(winLossService);
    }

    [Fact]
    public async Task Handle_GetWinLossQuery_CallsGetWinLoss()
    {
        // Arrange
        var query = new GetWinLossQuery(123456, UserLevel.Super.Value, DateTime.Today);

        // Act
        await winLossHandler.Handle(query, default);

        // Assert
        await winLossService.Received().GetWinLoss(
            Arg.Is<int>(custId => 123456 == custId),
            Arg.Is<UserLevel>(custLevel => UserLevel.Super == custLevel),
            Arg.Is<DateTime>(today => today == DateTime.Today)
        );
    }

    [Fact]
    public async Task Handle_GetDownlineWinLossQuery_CallsGetDownlineWinLoss()
    {
        // Arrange
        var query = new GetDownlineWinLossQuery(123456, UserLevel.Super.Value, DateTime.Today.AddDays(-1), 789);

        // Act
        await winLossHandler.Handle(query, default);

        // Assert
        await winLossService.Received().GetDownlineWinLoss(
            Arg.Is<int>(custId => 123456 == custId),
            Arg.Is<UserLevel>(custLevel => UserLevel.Super == custLevel),
            Arg.Is<DateTime>(yesterday => yesterday == DateTime.Today.AddDays(-1)),
            Arg.Is<int>(downlineId => 789 == downlineId)
        );
    }

    [Fact]
    public async Task Handle_GetTopUsernameWinLossQuery_CallsGetTopUsername()
    {
        // Arrange
        var query = new GetTopUsernameWinLossQuery(123456, UserLevel.Super.Value, DateTime.Today, 30);

        // Act
        await winLossHandler.Handle(query, default);

        // Assert
        await winLossService.Received().GetTopUsername(
            Arg.Is<int>(custId => 123456 == custId),
            Arg.Is<UserLevel>(custLevel => UserLevel.Super == custLevel),
            Arg.Is<DateTime>(today => today == DateTime.Today),
            Arg.Is<int>(top => 30 == top)
        );
    }
}
