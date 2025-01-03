using System.Collections.Generic;
using System.Threading.Tasks;
using Alpha.API.Handlers;
using Alpha.API.Requests.Customers;
using Alpha.API.Requests.Reports.Outstanding;
using Alpha.Customers.Params;
using Alpha.Reports.Params;
using Alpha.Reports.Services;
using Alpha.Shared.Enumerations;
using NSubstitute;
using Xunit;

namespace Alpha.API.Tests.Handlers;

public class OutstandingHandlerTests
{
    private readonly IOutstandingService outstandingService;
    private readonly OutstandingHandler outstandingHandler;

    public OutstandingHandlerTests()
    {
        outstandingService = Substitute.For<IOutstandingService>();
        outstandingHandler = new OutstandingHandler(outstandingService);
    }

    [Fact]
    public async Task Handle_GetDownlineOutstandingQuery_CallsGetDownlineOutstanding()
    {
        // Arrange
        var query = new GetDownlineOutstandingQuery(123456, UserLevel.Super.Value, 789, "en-US", "", UserLevel.Member.Value);

        // Act
        await outstandingHandler.Handle(query, default);

        // Assert
        await outstandingService.Received().GetDownlineOutstanding(Arg.Any<GetDownlineOutstandingParam>());
    }

    [Fact]
    public async Task Handle_GetTopOutstandingUsernameQuery_CallsGetTopUsername()
    {
        // Arrange
        var query = new GetTopOutstandingUsernameQuery(123456, UserLevel.Super.Value, 20);

        // Act
        await outstandingHandler.Handle(query, default);

        // Assert
        await outstandingService.Received().GetTopUsername(
            Arg.Is<int>(custId => 123456 == custId),
            Arg.Is<UserLevel>(custLevel => UserLevel.Super == custLevel),
            Arg.Is<int>(top => 20 == top)
        );
    }

    [Fact]
    public async Task Handle_GetTopOutstandingQuery_CallsGetTopOutstanding()
    {
        // Arrange
        var query = new GetTopOutstandingQuery(123456, UserLevel.Super.Value, 20);

        // Act
        await outstandingHandler.Handle(query, default);

        // Assert
        await outstandingService.Received().GetTopOutstanding(
            Arg.Is<int>(custId => 123456 == custId),
            Arg.Is<UserLevel>(custLevel => UserLevel.Super == custLevel),
            Arg.Is<int>(top => 20 == top)
        );
    }
}
