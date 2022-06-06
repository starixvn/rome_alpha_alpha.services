using System.Threading.Tasks;
using Alpha.API.Controllers;
using Alpha.API.Requests.Customers;
using Alpha.Customers.Params;
using MediatR;
using NSubstitute;
using Xunit;

namespace Alpha.API.Tests.Controllers;

public class CustomerControllerTests
{
    private readonly IMediator mediator;
    private readonly CustomerController customerController;

    public CustomerControllerTests()
    {
        mediator = Substitute.For<IMediator>();
        customerController = new CustomerController(mediator);
    }

    [Fact]
    public async Task GetCustomerStatusByBatch_Always_CallsMediatorSendGetCustomerStatusByBatchQuery()
    {
        // Arrange
        var param = new[]
        {
            new CustomerStatusParam(1, 0),
            new CustomerStatusParam(2, 0),
            new CustomerStatusParam(3, 0),
            new CustomerStatusParam(4, 5),
            new CustomerStatusParam(6, 7),
            new CustomerStatusParam(8, 9)
        };

        // Act
        await customerController.GetCustomerStatusByBatch(param);

        // Assert
        await mediator.Received().Send(Arg.Is<GetCustomerStatusByBatchQuery>(
            query => param == query.CustomerStatus)
        );
    }

    [Fact]
    public async Task GetCustomerStatus_Always_CallsMediatorSendGetCustomerStatusQuery()
    {
        // Arrange
        var custId = 123456;
        var subAccId = 789;
        var query = new GetCustomerStatusQuery(custId, subAccId);

        // Act
        await customerController.GetCustomerStatus(query);

        // Assert
        await mediator.Received().Send(Arg.Is<GetCustomerStatusQuery>(
            query => custId == query.CustId
            && subAccId == query.SubAccId)
        );
    }

    [Fact]
    public async Task GetDownlineInfo_Always_CallsMediatorSendGetDownlineInfoQuery()
    {
        // Arrange
        var custId = 123456;
        var downlineUsername = "agent01";
        var query = new GetDownlineInfoQuery(custId, downlineUsername);

        // Act
        await customerController.GetDownlineInfo(query);

        // Assert
        await mediator.Received().Send(Arg.Is<GetDownlineInfoQuery>(
            query => custId == query.CustId
            && downlineUsername == query.DownlineUsername)
        );
    }
}
