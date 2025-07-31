using Alpha.Customers.Consts;
using Alpha.Customers.Criteria;
using Alpha.Customers.Criteria.AutoTransfer;
using Alpha.Customers.Models;
using Alpha.Customers.Models.AutoTransfer;
using Alpha.Customers.Resources;
using Alpha.Customers.Services;
using Fanex.Data.Repository;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Alpha.Customers.Tests.Services;

public class AutoTransferServiceTests
{
    private readonly IDynamicRepository _mockRepository;
    private readonly AutoTransferService _service;

    public AutoTransferServiceTests()
    {
        _mockRepository = Substitute.For<IDynamicRepository>();
        _service = new AutoTransferService(_mockRepository);
    }

    [Fact]
    public async Task IsAllDayBalanceTransfer_WhenTrue_ReturnsTrue()
    {
        // Arrange
        const int custId = 123;
        _mockRepository.GetAsync<AllDayBalanceTransferModel>(Arg.Any<GetIsAllDayBalTransferCriteria>())
            .Returns(new AllDayBalanceTransferModel { IsAllDayBalTransfer = true });

        // Act
        var result = await _service.IsAllDayBalanceTransfer(custId);

        // Assert
        Assert.True(result);
        await _mockRepository.Received(1).GetAsync<AllDayBalanceTransferModel>(
            Arg.Is<GetIsAllDayBalTransferCriteria>(c => c.CustId == custId));
    }

    [Fact]
    public async Task IsAllDayBalanceTransfer_WhenFalse_ReturnsFalse()
    {
        // Arrange
        const int custId = 123;
        _mockRepository.GetAsync<AllDayBalanceTransferModel>(Arg.Any<GetIsAllDayBalTransferCriteria>())
            .Returns(new AllDayBalanceTransferModel { IsAllDayBalTransfer = false });

        // Act
        var result = await _service.IsAllDayBalanceTransfer(custId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task IsAllDayBalanceTransfer_WhenNull_ReturnsFalse()
    {
        // Arrange
        const int custId = 123;
        _mockRepository.GetAsync<AllDayBalanceTransferModel>(Arg.Any<GetIsAllDayBalTransferCriteria>())
            .Returns((AllDayBalanceTransferModel)null);

        // Act
        var result = await _service.IsAllDayBalanceTransfer(custId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task Monitor_WhenAllDayBalanceTransfer_ReturnsErrorModel()
    {
        // Arrange
        const int custId = 123;
        const string language = LanguageCodes.English;
        _mockRepository.GetAsync<AllDayBalanceTransferModel>(Arg.Any<GetIsAllDayBalTransferCriteria>())
            .Returns(new AllDayBalanceTransferModel { IsAllDayBalTransfer = true });

        // Act
        var result = await _service.Monitor(custId, "username", "status", 1, 1, 1, language);

        // Assert
        Assert.Equal(108, result.Code);
        Assert.Equal(ResourceHelper.Get(nameof(SharedResource.ADBTChangeError), language), result.Message);
    }

    [Fact]
    public async Task Monitor_WhenNotAllDayBalanceTransfer_ReturnsTransfers()
    {
        // Arrange
        const int custId = 123;
        _mockRepository.GetAsync<AllDayBalanceTransferModel>(Arg.Any<GetIsAllDayBalTransferCriteria>())
            .Returns(new AllDayBalanceTransferModel { IsAllDayBalTransfer = false });

        _mockRepository.GetAsync<SiteDBGroup>(Arg.Any<GetSiteDBConfigCriteria>())
            .Returns(new SiteDBGroup { SiteDBGroupID = 1 });

        var transfers = new List<AutoTransferMonitoringModel> { new AutoTransferMonitoringModel() };
        _mockRepository.FetchAsync<AutoTransferMonitoringModel>(Arg.Any<GetMonitorCriteria>())
            .Returns(transfers);

        // Act
        var result = await _service.Monitor(custId, "username", "status", 1, 1, 1);

        // Assert
        Assert.Equal(transfers, result.Transfers);
        Assert.Equal(0, result.Code);
    }

    [Fact]
    public async Task Customers_WhenAllDayBalanceTransfer_ReturnsErrorModel()
    {
        // Arrange
        const int custId = 123;
        const string language = LanguageCodes.English;
        _mockRepository.GetAsync<AllDayBalanceTransferModel>(Arg.Any<GetIsAllDayBalTransferCriteria>())
            .Returns(new AllDayBalanceTransferModel { IsAllDayBalTransfer = true });

        // Act
        var result = await _service.Customers(custId, 1, language, "username", "1", "status", 1, 10);

        // Assert
        Assert.Equal(108, result.Code);
        Assert.Equal(ResourceHelper.Get(nameof(SharedResource.ADBTChangeError), language), result.Message);
    }

    [Theory]
    [InlineData((int)CustomerLevel.Super)]
    [InlineData((int)CustomerLevel.Master)]
    [InlineData((int)CustomerLevel.Member)]
    public async Task Customers_WhenNotAllDayBalanceTransfer_ReturnsCustomers(int custLevel)
    {
        // Arrange
        const int custId = 123;
        _mockRepository.GetAsync<AllDayBalanceTransferModel>(Arg.Any<GetIsAllDayBalTransferCriteria>())
            .Returns(new AllDayBalanceTransferModel { IsAllDayBalTransfer = false });

        var customers = new List<AutoTransferCustomerModel>
            {
                new AutoTransferCustomerModel { Username = "UserB" },
                new AutoTransferCustomerModel { Username = "UserA" }
            };
        _mockRepository.FetchAsync<AutoTransferCustomerModel>(Arg.Any<GetCustomersCriteria>())
            .Returns(customers);

        _mockRepository.ExecuteAsync(Arg.Any<GetTransferConditionCriteria>())
            .Returns(Task.FromResult(0))
            .AndDoes(info => ((GetTransferConditionCriteria)info[0]).TransferOption = "Option");

        // Act
        var result = await _service.Customers(custId, custLevel, "en", "username", "1", "status", 1, 10);

        // Assert
        Assert.Equal(2, result.Customers.Count());
        Assert.Equal("UserA", result.Customers.First().Username); // Should be ordered
        Assert.Equal("Option", result.UplineTransferOption);
    }

    [Fact]
    public async Task GetTransferCondition_ReturnsCorrectCondition()
    {
        // Arrange
        const int custId = 123;
        const string expectedOption = "TestOption";

        _mockRepository.ExecuteAsync(Arg.Any<GetTransferConditionCriteria>())
            .Returns(Task.FromResult(0))
            .AndDoes(info => ((GetTransferConditionCriteria)info[0]).TransferOption = expectedOption);

        // Act
        var result = await _service.GetTransferCondition(custId);

        // Assert
        Assert.Equal(expectedOption, result);
        await _mockRepository.Received(1).ExecuteAsync(Arg.Is<GetTransferConditionCriteria>(c => c.CustId == custId));
    }

    [Fact]
    public async Task UpdateStatus_Success_ReturnsSuccessResult()
    {
        // Arrange
        const int uplineId = 100;
        const int custId = 123;
        const string username = "testuser";
        const bool state = true;
        const string language = LanguageCodes.English;

        _mockRepository.GetAsync<SiteDBGroup>(Arg.Any<GetSiteDBConfigCriteria>())
            .Returns(new SiteDBGroup { SiteDBGroupID = 1 });

        _mockRepository.ExecuteAsync(Arg.Any<GetJobStatusCriteria>())
            .Returns(Task.FromResult(0))
            .AndDoes(info => ((GetJobStatusCriteria)info[0]).IsAutoJobRunning = false);

        _mockRepository.ExecuteAsync(Arg.Any<UpdateStatusCriteria>())
            .Returns(Task.FromResult(0))
            .AndDoes(info =>
            {
                var criteria = (UpdateStatusCriteria)info[0];
                criteria.ErrCode = 0;
                criteria.ErrMsg = "";
            });

        // Act
        var result = await _service.UpdateStatus(uplineId, 0, 1, language, custId, username, state, "schedule", "oldOption");

        // Assert
        Assert.Equal(0, result.Code);
        var expectedMessage = string.Format(ResourceHelper.Get(nameof(SharedResource.AutoTransfer_UpdateSuccessOn), language), username);
        Assert.Equal(expectedMessage, result.Message);
    }

    [Fact]
    public async Task UpdateStatus_Warning_ReturnsWarningResult()
    {
        // Arrange
        const string warningMsg = "Warning message";

        _mockRepository.GetAsync<SiteDBGroup>(Arg.Any<GetSiteDBConfigCriteria>())
            .Returns(new SiteDBGroup { SiteDBGroupID = 1 });

        _mockRepository.ExecuteAsync(Arg.Any<GetJobStatusCriteria>())
            .Returns(Task.FromResult(0));

        _mockRepository.ExecuteAsync(Arg.Any<UpdateStatusCriteria>())
            .Returns(Task.FromResult(0))
            .AndDoes(info =>
            {
                var criteria = (UpdateStatusCriteria)info[0];
                criteria.ErrCode = 0;
                criteria.ErrMsg = warningMsg;
            });

        // Act
        var result = await _service.UpdateStatus(1, 0, 1, "en", 123, "user", true, "schedule", "oldOption");

        // Assert
        Assert.Equal(-1, result.Code);
        Assert.Equal(warningMsg, result.Message);
    }

    [Fact]
    public async Task UpdateSchedule_Success_ReturnsSuccessResult()
    {
        // Arrange
        const int uplineId = 100;
        const int custId = 123;
        const string username = "testuser";
        const string language = LanguageCodes.English;

        _mockRepository.GetAsync<SiteDBGroup>(Arg.Any<GetSiteDBConfigCriteria>())
            .Returns(new SiteDBGroup { SiteDBGroupID = 1 });

        _mockRepository.ExecuteAsync(Arg.Any<GetJobStatusCriteria>())
            .Returns(Task.FromResult(0));

        _mockRepository.ExecuteAsync(Arg.Any<UpdateScheduleCriteria>())
            .Returns(Task.FromResult(0))
            .AndDoes(info =>
            {
                var criteria = (UpdateScheduleCriteria)info[0];
                criteria.ErrCode = 0;
                criteria.ErrMsg = "";
            });

        // Act
        var result = await _service.UpdateSchedule(uplineId, 0, 1, language, custId, username, "schedule", "oldOption");

        // Assert
        Assert.Equal(0, result.Code);
        var expectedMessage = string.Format(ResourceHelper.Get(nameof(SharedResource.AutoTransfer_UpdateScheduleSuccess), language), username);
        Assert.Equal(expectedMessage, result.Message);
    }

    [Fact]
    public async Task UpdateMultiple_Success_UpdatesAllAccounts()
    {
        // Arrange
        const int uplineId = 100;
        const string language = LanguageCodes.English;
        string onIds = "1,2";
        string offIds = "3,4";
        string onNames = "User1,User2";
        string offNames = "User3,User4";

        _mockRepository.GetAsync<SiteDBGroup>(Arg.Any<GetSiteDBConfigCriteria>())
            .Returns(new SiteDBGroup { SiteDBGroupID = 1 });

        _mockRepository.ExecuteAsync(Arg.Any<GetJobStatusCriteria>())
            .Returns(Task.FromResult(0));

        _mockRepository.ExecuteAsync(Arg.Any<UpdateStatusCriteria>())
            .Returns(Task.FromResult(0))
            .AndDoes(info =>
            {
                var criteria = (UpdateStatusCriteria)info[0];
                criteria.ErrCode = 0;
                criteria.ErrMsg = "";
            });

        // Act
        var result = await _service.UpdateMultiple(
            uplineId, 0, 1, language,
            onIds, offIds, onNames, offNames,
            "onSchedule", "offSchedule", true, false, "oldOption");

        // Assert
        Assert.Empty(result.WarningCusts);
        Assert.Empty(result.ErrMessages);
        Assert.Empty(result.ErrCusts);
        Assert.Null(result.StopMessage);
        Assert.Null(result.Code);
    }

    [Fact]
    public async Task UpdateMultiple_WithWarning_CollectsWarnings()
    {
        // Arrange
        const int uplineId = 100;
        const string language = LanguageCodes.English;
        string ids = "1,2";
        string names = "User1,User2";

        _mockRepository.GetAsync<SiteDBGroup>(Arg.Any<GetSiteDBConfigCriteria>())
            .Returns(new SiteDBGroup { SiteDBGroupID = 1 });

        _mockRepository.ExecuteAsync(Arg.Any<GetJobStatusCriteria>())
            .Returns(Task.FromResult(0));

        int callCount = 0;
        _mockRepository.ExecuteAsync(Arg.Any<UpdateStatusCriteria>())
            .Returns(Task.FromResult(0))
            .AndDoes(info =>
            {
                var criteria = (UpdateStatusCriteria)info[0];
                callCount++;

                // First call has a warning
                if (callCount == 1)
                {
                    criteria.ErrCode = 0;
                    criteria.ErrMsg = "Warning";
                }
                else
                {
                    criteria.ErrCode = 0;
                    criteria.ErrMsg = "";
                }
            });

        // Act
        var result = await _service.UpdateMultiple(
            uplineId, 0, 1, language,
            "", ids, "", names,
            "", "schedule", true, false, "oldOption");

        // Assert
        Assert.Single(result.WarningCusts);
        Assert.Equal("User1", result.WarningCusts[0]);
    }

    [Fact]
    public async Task UpdateMultiple_WithError_CollectsErrors()
    {
        // Arrange
        const int uplineId = 100;
        const string language = LanguageCodes.English;
        string ids = "1,2";
        string names = "User1,User2";

        _mockRepository.GetAsync<SiteDBGroup>(Arg.Any<GetSiteDBConfigCriteria>())
            .Returns(new SiteDBGroup { SiteDBGroupID = 1 });

        _mockRepository.ExecuteAsync(Arg.Any<GetJobStatusCriteria>())
            .Returns(Task.FromResult(0));

        int callCount = 0;
        _mockRepository.ExecuteAsync(Arg.Any<UpdateStatusCriteria>())
            .Returns(Task.FromResult(0))
            .AndDoes(info =>
            {
                var criteria = (UpdateStatusCriteria)info[0];
                callCount++;

                // First call has an error
                if (callCount == 1)
                {
                    criteria.ErrCode = 100;
                    criteria.ErrMsg = "Error message";
                }
                else
                {
                    criteria.ErrCode = 0;
                    criteria.ErrMsg = "";
                }
            });

        // Act
        var result = await _service.UpdateMultiple(
            uplineId, 0, 1, language,
            "", ids, "", names,
            "", "schedule", true, false, "oldOption");

        // Assert
        Assert.Single(result.ErrMessages);
        Assert.Equal("Error message", result.ErrMessages["100"]);
        Assert.Single(result.ErrCusts["100"]);
        Assert.Equal("User1", result.ErrCusts["100"][0]);
    }

    [Fact]
    public async Task UpdateMultiple_WithStopError_StopsProcessing()
    {
        // Arrange
        const int uplineId = 100;
        const string language = LanguageCodes.English;
        string ids = "1,2";
        string names = "User1,User2";

        _mockRepository.GetAsync<SiteDBGroup>(Arg.Any<GetSiteDBConfigCriteria>())
            .Returns(new SiteDBGroup { SiteDBGroupID = 1 });

        _mockRepository.ExecuteAsync(Arg.Any<GetJobStatusCriteria>())
            .Returns(Task.FromResult(0));

        _mockRepository.ExecuteAsync(Arg.Any<UpdateStatusCriteria>())
            .Returns(Task.FromResult(0))
            .AndDoes(info =>
            {
                var criteria = (UpdateStatusCriteria)info[0];
                criteria.ErrCode = 108; // Stop code
                criteria.ErrMsg = "Stop error";
            });

        // Act
        var result = await _service.UpdateMultiple(
            uplineId, 0, 1, language,
            ids, ids, names, names,
            "schedule", "schedule", true, false, "oldOption");

        // Assert
        Assert.Equal(108, result.Code);
        Assert.Equal("Stop error", result.StopMessage);
        // When we hit a stop error, we shouldn't process any more records
        await _mockRepository.Received(1).ExecuteAsync(Arg.Any<UpdateStatusCriteria>());
    }
}
