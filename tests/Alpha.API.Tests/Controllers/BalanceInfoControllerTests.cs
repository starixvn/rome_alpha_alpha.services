using System.Threading.Tasks;
using Alpha.API.Controllers;
using Alpha.API.Requests.Reports.BalanceInfo;
using Alpha.Shared.Enumerations;
using MediatR;
using NSubstitute;
using Xunit;

namespace Alpha.API.Tests.Controllers;

public class BalanceInfoControllerTests
{
    private readonly IMediator mediator;
    private readonly BalanceInfoController balanceInfoController;

    public BalanceInfoControllerTests()
    {
        mediator = Substitute.For<IMediator>();
        balanceInfoController = new BalanceInfoController(mediator);
    }

    [Fact]
    public async Task GetCustomerBalance_Always_CallsMediatorSendGetCustomerBalanceQuery()
    {
        // Arrange
        var custId = 1176070;
        var custLevelId = UserLevel.Super.Value;
        var query = new GetCustomerBalanceQuery(custId, custLevelId);

        // Act
        await balanceInfoController.GetCustomerBalance(query);

        // Assert
        await mediator.Received().Send(Arg.Is<GetCustomerBalanceQuery>(
            query => custId == query.CustId
            && custLevelId == query.CustLevelId)
        );
    }

    [Fact]
    public async Task GetDownlineBalance_Always_CallsMediatorSendGetDownlineBalanceQuery()
    {
        // Arrange
        var custId = 1176070;
        var custLevelId = UserLevel.Super.Value;
        var query = new GetDownlineBalanceQuery(custId, custLevelId);

        // Act
        await balanceInfoController.GetDownlineBalance(query);

        // Assert
        await mediator.Received().Send(Arg.Is<GetDownlineBalanceQuery>(
            query => custId == query.CustId
            && custLevelId == query.CustLevelId)
        );
    }

    [Fact]
    public async Task GetTopUsername_Always_CallsMediatorSendGetTopBalanceUsernameQuery()
    {
        // Arrange
        var custId = 1176070;
        var custLevelId = UserLevel.Super.Value;
        var top = 40;
        var query = new GetTopBalanceUsernameQuery(custId, custLevelId, top);

        // Act
        await balanceInfoController.GetTopUsername(query);

        // Assert
        await mediator.Received().Send(Arg.Is<GetTopBalanceUsernameQuery>(
            query => custId == query.CustId
            && custLevelId == query.CustLevelId
            && top == query.Top)
        );
    }

    [Fact]
    public async Task GetTopBalance_Always_CallsMediatorSendGetTopBalanceQuery()
    {
        // Arrange
        var custId = 1176070;
        var custLevelId = UserLevel.Super.Value;
        var top = 40;
        var query = new GetTopBalanceQuery(custId, custLevelId, top);

        // Act
        await balanceInfoController.GetTopBalance(query);

        // Assert
        await mediator.Received().Send(Arg.Is<GetTopBalanceQuery>(
            query => custId == query.CustId
            && custLevelId == query.CustLevelId
            && top == query.Top)
        );
    }
}
