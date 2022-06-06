using System.Collections.Generic;
using System.Threading.Tasks;
using Alpha.Customers.Configurations;
using Alpha.Customers.Criteria;
using Alpha.Customers.Models;
using Alpha.Customers.Params;
using Alpha.Customers.Services;
using Fanex.Caching;
using Fanex.Data.Repository;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace Alpha.Customers.Tests.Services;

public class CustomerServiceTests
{
    private const string CustomerStatusCacheKey = "Alpha.Customers.CustomerStatus";

    private readonly ICacheService cacheService;
    private readonly CustomerAppSettings appSettings;
    private readonly IDynamicRepository dynamicRepository;
    private readonly ICustomerService customerService;

    public CustomerServiceTests()
    {
        cacheService = Substitute.For<ICacheService>();
        appSettings = new CustomerAppSettings { CustomerStatusCacheInSeconds = 120 };
        IOptions<CustomerAppSettings> options = Options.Create(appSettings);
        dynamicRepository = Substitute.For<IDynamicRepository>();
        customerService = new CustomerService(cacheService, options, dynamicRepository);
    }

    [Fact]
    public async Task GetCustomerStatus_ValidCommand_CallsFetchAsync()
    {
        // Arrange
        var customers = new List<CustomerStatusParam>
        {
            new CustomerStatusParam(123456, 0),
            new CustomerStatusParam(123456, 789)
        };

        // Act
        await customerService.GetCustomerStatus(customers);

        // Assert
        await dynamicRepository.Received().FetchAsync<CustomerStatus>(Arg.Is<GetCustomerStatusCriteria>(
            criteria => "<Root><r id=\"123456\" sub=\"0\"/><r id=\"123456\" sub=\"789\"/></Root>" == criteria.CustList
        ));
    }

    [Fact]
    public async Task GetDownlineInfo_ValidCommand_CallsExecuteAsync()
    {
        // Arrange
        var uplineId = 123456;
        var downlineUsername = "master1";

        // Act
        await customerService.GetDownlineInfo(uplineId, downlineUsername);

        // Assert
        await dynamicRepository.Received().ExecuteAsync(Arg.Is<GetDownlineInfoCommand>(
            cmd => uplineId == cmd.UplineId
            && 0 == cmd.SubAccountId
            && downlineUsername == cmd.Username
        ));
    }
}
