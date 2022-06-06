using System;
using System.Threading.Tasks;
using Alpha.API.Controllers;
using Alpha.API.Requests.Reports.WinLoss;
using Alpha.Shared.Enumerations;
using MediatR;
using NSubstitute;
using Xunit;

namespace Alpha.API.Tests.Controllers;

public class WinLossControllerTests
{
    private readonly IMediator mediator;
    private readonly WinLossController winLossController;

    public WinLossControllerTests()
    {
        mediator = Substitute.For<IMediator>();
        winLossController = new WinLossController(mediator);
    }

    [Fact]
    public async Task GetWinLoss_Always_CallsMediatorSendGetWinLossQuery()
    {
        // Arrange
        var custId = 1176070;
        var custLevelId = UserLevel.Super.Value;
        var today = DateTime.Today;
        var query = new GetWinLossQuery(custId, custLevelId, today);

        // Act
        await winLossController.GetWinLoss(query);

        // Assert
        await mediator.Received().Send(Arg.Is<GetWinLossQuery>(
            query => custId == query.CustId
            && custLevelId == query.CustLevelId
            && today == query.Date)
        );
    }

    [Fact]
    public async Task GetDownlineWinLoss_Always_CallsMediatorSendGetDownlineWinLossQuery()
    {
        // Arrange
        var custId = 1176070;
        var custLevelId = UserLevel.Super.Value;
        var today = DateTime.Today;
        var downlineId = 789;
        var query = new GetDownlineWinLossQuery(custId, custLevelId, today, downlineId);

        // Act
        await winLossController.GetDownlineWinLoss(query);

        // Assert
        await mediator.Received().Send(Arg.Is<GetDownlineWinLossQuery>(
            query => custId == query.CustId
            && custLevelId == query.CustLevelId
            && today == query.Date
            && downlineId == query.DownlineId)
        );
    }

    [Fact]
    public async Task GetTopUsername_Always_CallsMediatorSendGetTopUsernameWinLossQuery()
    {
        // Arrange
        var custId = 1176070;
        var custLevelId = UserLevel.Super.Value;
        var yesterday = DateTime.Today.AddDays(-1);
        var top = 100;
        var query = new GetTopUsernameWinLossQuery(custId, custLevelId, yesterday, top);

        // Act
        await winLossController.GetTopUsername(query);

        // Assert
        await mediator.Received().Send(Arg.Is<GetTopUsernameWinLossQuery>(
            query => custId == query.CustId
            && custLevelId == query.CustLevelId
            && yesterday == query.Date
            && top == query.Top)
        );
    }
}
