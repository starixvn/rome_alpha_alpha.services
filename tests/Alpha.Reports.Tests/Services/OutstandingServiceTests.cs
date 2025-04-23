using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alpha.Reports.Configurations;
using Alpha.Reports.Criteria.Outstanding;
using Alpha.Reports.Models.Outstanding;
using Alpha.Reports.Params;
using Alpha.Reports.Services;
using Alpha.Shared.Enumerations;
using Alpha.Shared.Extensions;
using Alpha.Shared.Services;
using Alpha.Shared.Utils;
using Fanex.Data.Repository;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace Alpha.Reports.Tests.Services;

public class OutstandingServiceTests
{
    private readonly IProductService productService;
    private readonly ICacheRefService cacheRefService;
    private readonly ReportAppSettings appSettings;
    private readonly IDynamicRepository dynamicRepository;
    private readonly IOutstandingService outstandingService;

    public OutstandingServiceTests()
    {
        productService = Substitute.For<IProductService>();
        cacheRefService = Substitute.For<ICacheRefService>();
        appSettings = new ReportAppSettings
        {
            SettingStatus = 1,
            BalanceUpDayAndMoveBetsDayCacheInSeconds = 120,
            OfferedProductsByCustIdCacheInSeconds = 300
        };
        IOptions<ReportAppSettings> options = Options.Create(appSettings);
        dynamicRepository = Substitute.For<IDynamicRepository>();
        outstandingService = new OutstandingService(productService, cacheRefService, options, dynamicRepository);
    }

    [Fact]
    public async Task GetDownlineOutstanding_Always_CallsFetchAsync()
    {
        // Arrange
        var custId = 1176070;
        var custLevel = UserLevel.Super;
        var downlineId = 1176083;
        byte settingStatus = 1;
        var minReportDate = SystemTime.Today;
        IEnumerable<int> productIds = new List<int> { 1, 2, 3, 4, 5, 6 };
        productService.GetOfferedProducts(custId, settingStatus, minReportDate).Returns(Task.FromResult(productIds));

        // Act
        await outstandingService.GetDownlineOutstanding(new GetDownlineOutstandingParam(
            custId,
            custLevel,
            downlineId,
            "en-US",
            "",
            UserLevel.Member
        ));

        // Assert
        await dynamicRepository.Received().FetchAsync<OutstandingEntity>(Arg.Is<GetDownlineOutstandingCriteria>(
            criteria => custLevel == criteria.CustLevel
            && productIds.JoinWithComma(",") == criteria.ProductIds
            && downlineId == criteria.DownlineId
        ));
    }

    [Fact]
    public async Task GetDownlineOutstanding_HasData_ReturnsCorrectModel()
    {
        // Arrange
        var custId = 1176070;
        var custLevel = UserLevel.Super;
        var downlineId = 1176083;
        byte settingStatus = 1;
        var minReportDate = SystemTime.Today;
        IEnumerable<int> productIds = new List<int> { 1, 2, 3, 4, 5, 6 };
        productService.GetOfferedProducts(custId, settingStatus, minReportDate).Returns(Task.FromResult(productIds));

        IEnumerable<OutstandingEntity> outstandings = new List<OutstandingEntity>
        {
            new OutstandingEntity(1, "991", 51_700, null, null, 10_340, null, null, null, null),
            new OutstandingEntity(2, "1002", 570_614, null, null, 114_498.70M, null, null, null, null)
        };
        dynamicRepository.FetchAsync<OutstandingEntity>(Arg.Any<GetDownlineOutstandingCriteria>()).Returns(Task.FromResult(outstandings));

        // Act
        var report = await outstandingService.GetDownlineOutstanding(new GetDownlineOutstandingParam(
            custId,
            custLevel,
            downlineId,
            "en-US",
            "",
            UserLevel.Member
        ));

        // Assert
        Assert.NotEmpty(report.Outstandings);
        Assert.True(622_314 == report.Total.MemberOutstanding
            && 124_838.70M == report.Total.Outstanding);
    }

    [Fact]
    public async Task GetDownlineOutstanding_NoData_ReturnsEmptyModel()
    {
        // Arrange
        var custId = 1176070;
        var custLevel = UserLevel.Super;
        var downlineId = 1176083;
        byte settingStatus = 1;
        var minReportDate = SystemTime.Today;
        IEnumerable<int> productIds = new List<int> { 1, 2, 3, 4, 5, 6 };
        productService.GetOfferedProducts(custId, settingStatus, minReportDate).Returns(Task.FromResult(productIds));

        var outstandings = Enumerable.Empty<OutstandingEntity>();
        dynamicRepository.FetchAsync<OutstandingEntity>(Arg.Any<GetDownlineOutstandingCriteria>()).Returns(Task.FromResult(outstandings));

        // Act
        var report = await outstandingService.GetDownlineOutstanding(new GetDownlineOutstandingParam(
            custId,
            custLevel,
            downlineId,
            "en-US",
            "",
            UserLevel.Member
        ));

        // Assert
        Assert.Empty(report.Outstandings);
        Assert.Null(report.Total);
    }

    [Fact]
    public async Task GetTopUsername_HasData_ReturnsCorrectUsernameList()
    {
        // Arrange
        var custId = 1176070;
        var custLevel = UserLevel.Super;
        var top = 10;
        byte settingStatus = 1;
        var minReportDate = SystemTime.Today;
        IEnumerable<int> productIds = new List<int> { 1, 2, 3, 4, 5, 6 };
        productService.GetOfferedProducts(custId, settingStatus, minReportDate).Returns(Task.FromResult(productIds));

        IEnumerable<OutstandingEntity> outStandings = new List<OutstandingEntity>
        {
            new OutstandingEntity(1, "991", 51_700, null, null, 10_340, null, null, null, null),
            new OutstandingEntity(2, "1002", 570_614, null, null, 114_498.70M, null, null, null, null)
        };
        dynamicRepository.FetchAsync<OutstandingEntity>(Arg.Any<GetTopOutstandingCommand>()).Returns(Task.FromResult(outStandings));

        // Act
        var usernames = await outstandingService.GetTopUsername(custId, custLevel, top);

        // Assert
        Assert.True("991,1002" == usernames.JoinWithComma());
    }

    [Fact]
    public async Task GetCustomerOutstanding_Always_CallsGetAsync()
    {
        // Arrange
        var custId = 1176070;
        byte settingStatus = 1;
        var minReportDate = SystemTime.Today;
        IEnumerable<int> productIds = new List<int> { 1, 2, 3, 4, 5, 6 };
        productService.GetOfferedProducts(custId, settingStatus, minReportDate).Returns(Task.FromResult(productIds));

        // Act
        await outstandingService.GetCustomerOutstanding(custId);

        // Assert
        await dynamicRepository.Received().GetAsync<CustomerOutstanding>(Arg.Is<GetCustomerOutstandingCriteria>(
            criteria => custId == criteria.CustId
        ));
    }
}