using Alpha.Reports.Configurations;
using Alpha.Reports.Criteria.WinLoss;
using Alpha.Reports.Models;
using Alpha.Reports.Models.WinLoss;
using Alpha.Reports.Services;
using Alpha.Shared.Enumerations;
using Fanex.Caching;
using Fanex.Data.Repository;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Alpha.Reports.Tests.Services
{
    public class WinLossServiceTests
    {
        private readonly IProductService _productService;
        private readonly IReportDateService _reportDateService;
        private readonly IOptions<ReportAppSettings> _options;
        private readonly IDynamicRepository _dynamicRepository;
        private readonly ICacheService _cacheService;
        private readonly WinLossService _service;
        private readonly ReportAppSettings _appSettings;

        public WinLossServiceTests()
        {
            _productService = Substitute.For<IProductService>();
            _reportDateService = Substitute.For<IReportDateService>();
            _appSettings = new ReportAppSettings
            {
                SettingStatus = 1,
                CashServiceOfferedSites = "site1,site2",
                LocalTest = true
            };
            _options = Substitute.For<IOptions<ReportAppSettings>>();
            _options.Value.Returns(_appSettings);
            _dynamicRepository = Substitute.For<IDynamicRepository>();
            _cacheService = Substitute.For<ICacheService>();

            _service = new WinLossService(
                _productService,
                _reportDateService,
                _options,
                _dynamicRepository,
                _cacheService);
        }

        [Fact]
        public async Task GetWinLoss_WhenUserIsSuper_ShouldReturnSumWinLossMaster()
        {
            // Arrange
            var custId = 123;
            var custLevel = UserLevel.Super;
            var date = new DateTime(2023, 1, 1);
            var limitDate = new DateTime(2022, 12, 1);

            _reportDateService.GetLimitDate(custId).Returns(limitDate);
            _reportDateService.GetBalanceUpDayAndMoveBetsDay().Returns(Task.FromResult(new BalanceUpDayAndMoveBetsDay(new DateTime(2022, 12, 20), new DateTime(2022, 12, 10))));
            _productService.GetOfferedProducts(custId, _appSettings.SettingStatus, Arg.Any<DateTime>(), _appSettings.CashServiceOfferedSites)
                .Returns(Task.FromResult<IEnumerable<int>>(new int[] { 1, 2 }));

            var winLossData = new List<WinLossMaster>
            {
                new WinLossMaster { CustId = 1, CustName = "Master1", MasterTotal = 100 },
                new WinLossMaster { CustId = 2, CustName = "Master2", MasterTotal = 200 }
            };

            _dynamicRepository.FetchAsync<WinLossMaster>(Arg.Any<GetWinLossCriteria>()).Returns(winLossData);

            // Act
            var result = await _service.GetWinLoss(custId, custLevel, date);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<WinLossMaster>(result);
            var masterResult = (WinLossMaster)result;
            Assert.Equal(300, masterResult.MasterTotal);
        }

        [Fact]
        public async Task GetWinLoss_WhenUserIsMaster_ShouldReturnSumWinLossAgent()
        {
            // Arrange
            var custId = 123;
            var custLevel = UserLevel.Master;
            var date = new DateTime(2023, 1, 1);
            var limitDate = new DateTime(2022, 12, 1);

            _reportDateService.GetLimitDate(custId).Returns(limitDate);
            _reportDateService.GetBalanceUpDayAndMoveBetsDay().Returns(Task.FromResult(new BalanceUpDayAndMoveBetsDay(new DateTime(2022, 12, 20), new DateTime(2022, 12, 10))));
            _productService.GetOfferedProducts(custId, _appSettings.SettingStatus, Arg.Any<DateTime>(), _appSettings.CashServiceOfferedSites)
                .Returns(Task.FromResult<IEnumerable<int>>(new int[] { 1, 2 }));

            var winLossData = new List<WinLossAgent>
            {
                new WinLossAgent { CustId = 1, CustName = "Agent1", AgentTotal = 100 },
                new WinLossAgent { CustId = 2, CustName = "Agent2", AgentTotal = 200 }
            };

            _dynamicRepository.FetchAsync<WinLossAgent>(Arg.Any<GetWinLossCriteria>()).Returns(winLossData);

            // Act
            var result = await _service.GetWinLoss(custId, custLevel, date);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<WinLossAgent>(result);
            var agentResult = (WinLossAgent)result;
            Assert.Equal(300, agentResult.AgentTotal);
        }

        [Fact]
        public async Task GetWinLoss_WhenDateIsLessThanLimitDate_ShouldReturnNull()
        {
            // Arrange
            var custId = 123;
            var custLevel = UserLevel.Agent;
            var date = new DateTime(2022, 1, 1);
            var limitDate = new DateTime(2022, 12, 1);

            _reportDateService.GetLimitDate(custId).Returns(limitDate);

            // Act
            var result = await _service.GetWinLoss(custId, custLevel, date);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetTopUsername_WhenUserIsSuper_ShouldReturnTopMasterNames()
        {
            // Arrange
            var custId = 123;
            var custLevel = UserLevel.Super;
            var date = new DateTime(2023, 1, 1);
            var top = 2;
            var limitDate = new DateTime(2022, 12, 1);

            _reportDateService.GetLimitDate(custId).Returns(limitDate);
            _reportDateService.GetBalanceUpDayAndMoveBetsDay().Returns(Task.FromResult(new BalanceUpDayAndMoveBetsDay(new DateTime(2022, 12, 20), new DateTime(2022, 12, 10))));
            _productService.GetOfferedProducts(custId, _appSettings.SettingStatus, Arg.Any<DateTime>(), _appSettings.CashServiceOfferedSites)
                .Returns(Task.FromResult<IEnumerable<int>>(new int[] { 1, 2 }));

            var winLossData = new List<WinLossMaster>
            {
                new WinLossMaster { CustId = 1, CustName = "Master1", MasterTotal = 100 },
                new WinLossMaster { CustId = 2, CustName = "Master2", MasterTotal = 200 },
                new WinLossMaster { CustId = 3, CustName = "Master3", MasterTotal = 50 },
                new WinLossMaster { CustId = 4, CustName = "CASH SERVICE BILLING", MasterTotal = 1000 }
            };

            _dynamicRepository.FetchAsync<WinLossMaster>(Arg.Any<GetWinLossCriteria>()).Returns(winLossData);

            // Act
            var result = await _service.GetTopUsername(custId, custLevel, date, top);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains("Master2", result);
            Assert.Contains("Master1", result);
            Assert.DoesNotContain("CASH SERVICE BILLING", result);
        }

        [Fact]
        public async Task GetYesterdayEnteredSportResult_ShouldReturnCachedData()
        {
            // Arrange
            var enteredSportData = new List<DailyEnteredSport>
            {
                new DailyEnteredSport { IsToday = false, Entered = 10, NonEntered = 5 },
                new DailyEnteredSport { IsToday = false, Entered = 15, NonEntered = 7 }
            };

            _cacheService.GetOrSetAsync(
                "Alpha.Reports.Services.WinLossService.EnteredResults",
                Arg.Any<Func<Task<IEnumerable<DailyEnteredSport>>>>(),
                Arg.Any<CacheItemOptions>())
                .Returns(enteredSportData);

            // Act
            var result = await _service.GetYesterdayEnteredSportResult();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(25, result.Entered);
            Assert.Equal(12, result.NonEntered);
        }

        [Fact]
        public async Task GetDownlineWinLoss_WhenUserIsSuper_ShouldReturnSumWinLossMaster()
        {
            // Arrange
            var custId = 123;
            var custLevel = UserLevel.Super;
            var date = new DateTime(2023, 1, 1);
            var downlineId = 456;
            var limitDate = new DateTime(2022, 12, 1);

            _reportDateService.GetLimitDate(custId).Returns(limitDate);
            _reportDateService.GetBalanceUpDayAndMoveBetsDay().Returns(Task.FromResult(new BalanceUpDayAndMoveBetsDay(new DateTime(2022, 12, 20), new DateTime(2022, 12, 10))));
            _productService.GetOfferedProducts(custId, _appSettings.SettingStatus, Arg.Any<DateTime>(), _appSettings.CashServiceOfferedSites)
                .Returns(Task.FromResult<IEnumerable<int>>(new int[] { 1, 2 }));

            var winLossData = new List<WinLossMaster>
            {
                new WinLossMaster { CustId = downlineId, CustName = "Master1", MasterTotal = 100 },
                new WinLossMaster { CustId = downlineId, CustName = "Master2", MasterTotal = 200 }
            };

            _dynamicRepository.FetchAsync<WinLossMaster>(Arg.Any<GetWinLossCriteria>()).Returns(winLossData);

            // Act
            var result = await _service.GetDownlineWinLoss(custId, custLevel, date, downlineId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<WinLossMaster>(result);
            var masterResult = (WinLossMaster)result;
            Assert.Equal(300, masterResult.MasterTotal);
        }
    }
}