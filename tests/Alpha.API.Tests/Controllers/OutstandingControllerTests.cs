using System.Threading.Tasks;
using Alpha.API.Controllers;
using Alpha.API.Requests.Reports.Outstanding;
using Alpha.Shared.Enumerations;
using MediatR;
using NSubstitute;
using Xunit;

namespace Alpha.API.Tests.Controllers;

public class OutstandingControllerTests
{
    private readonly IMediator mediator;
    private readonly OutstandingController outstandingController;

    public OutstandingControllerTests()
    {
        mediator = Substitute.For<IMediator>();
        outstandingController = new OutstandingController(mediator);
    }

    [Fact]
    public async Task GetDownlineOutstanding_Always_CallsMediatorSendGetDownlineOutstandingQuery()
    {
        // Arrange
        var custId = 1176070;
        var custLevelId = UserLevel.Super.Value;
        var downlineId = 789;
        var language = "en-US";
        var query = new GetDownlineOutstandingQuery(custId, custLevelId, downlineId, language);

        // Act
        await outstandingController.GetDownlineOutstanding(query);

        // Assert
        await mediator.Received().Send(Arg.Is<GetDownlineOutstandingQuery>(
            query => custId == query.CustId
            && custLevelId == query.CustLevelId
            && downlineId == query.DownlineId
            && language == query.Language)
        );
    }

    [Fact]
    public async Task GetTopUsername_Always_CallsMediatorSendGetTopOutstandingUsernameQuery()
    {
        // Arrange
        var custId = 1176070;
        var custLevelId = UserLevel.Super.Value;
        var top = 70;
        var query = new GetTopOutstandingUsernameQuery(custId, custLevelId, top);

        // Act
        await outstandingController.GetTopUsername(query);

        // Assert
        await mediator.Received().Send(Arg.Is<GetTopOutstandingUsernameQuery>(
            query => custId == query.CustId
            && custLevelId == query.CustLevelId
            && top == query.Top)
        );
    }

    [Fact]
    public async Task GetTopOutstanding_Always_CallsMediatorSendGetTopOutstandingQuery()
    {
        // Arrange
        var custId = 1176070;
        var custLevelId = UserLevel.Super.Value;
        var top = 70;
        var query = new GetTopOutstandingQuery(custId, custLevelId, top);

        // Act
        await outstandingController.GetTopOutstanding(query);

        // Assert
        await mediator.Received().Send(Arg.Is<GetTopOutstandingQuery>(
            query => custId == query.CustId
            && custLevelId == query.CustLevelId
            && top == query.Top)
        );
    }
}
