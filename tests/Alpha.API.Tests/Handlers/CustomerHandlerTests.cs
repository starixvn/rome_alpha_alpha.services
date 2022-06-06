using System.Collections.Generic;
using System.Threading.Tasks;
using Alpha.API.Handlers;
using Alpha.API.Requests.Customers;
using Alpha.API.Requests.Reports.BalanceInfo;
using Alpha.Customers.Params;
using Alpha.Customers.Services;
using Alpha.Reports.Services;
using Alpha.Shared.Enumerations;
using NSubstitute;
using Xunit;

namespace Alpha.API.Tests.Handlers;

public class CustomerHandlerTests
{
    private readonly ICustomerService customerService;
    private readonly CustomerHandler customerHandler;

    public CustomerHandlerTests()
    {
        customerService = Substitute.For<ICustomerService>();
        customerHandler = new CustomerHandler(customerService);
    }

    [Fact]
    public async Task Handle_GetCustomerStatusByBatchQuery_CallsGetCustomerStatus()
    {
        // Arrange
        var query = new GetCustomerStatusByBatchQuery(new[]
        {
            new CustomerStatusParam(123456, 0)
        });

        // Act
        await customerHandler.Handle(query, default);

        // Assert
        await customerService.Received().GetCustomerStatus(Arg.Any<IEnumerable<CustomerStatusParam>>());
    }

    [Fact]
    public async Task Handle_GetCustomerStatusQuery_CallsGetCustomerStatus()
    {
        // Arrange
        var query = new GetCustomerStatusQuery(123456, 789);

        // Act
        await customerHandler.Handle(query, default);

        // Assert
        await customerService.Received().GetCustomerStatus(Arg.Any<CustomerStatusParam>());
    }

    [Fact]
    public async Task Handle_GetDownlineInfoQuery_CallsGetDownlineInfo()
    {
        // Arrange
        var query = new GetDownlineInfoQuery(123456, "master1");

        // Act
        await customerHandler.Handle(query, default);

        // Assert
        await customerService.Received().GetDownlineInfo(
            Arg.Is<int>(custId => 123456 == custId),
            Arg.Is<string>(downlineUsername => "master1" == downlineUsername)
        );
    }
}
