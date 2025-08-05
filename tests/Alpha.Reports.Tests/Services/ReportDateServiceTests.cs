using Alpha.Reports.Configurations;
using Alpha.Reports.Criteria;
using Alpha.Reports.Models;
using Alpha.Reports.Services;
using Fanex.Caching;
using Fanex.Data.Repository;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Alpha.Reports.Tests.Services
{
    public class ReportDateServiceTests
    {
        private readonly ICacheService _cacheService;
        private readonly IOptions<ReportAppSettings> _options;
        private readonly IDynamicRepository _dynamicRepository;
        private readonly ReportDateService _service;
        private readonly ReportAppSettings _appSettings;

        public ReportDateServiceTests()
        {
            _cacheService = Substitute.For<ICacheService>();
            _appSettings = new ReportAppSettings
            {
                BalanceUpDayAndMoveBetsDayCacheInSeconds = 300,
                CustomerLimitDateCacheInSeconds = 600
            };
            _options = Substitute.For<IOptions<ReportAppSettings>>();
            _options.Value.Returns(_appSettings);
            _dynamicRepository = Substitute.For<IDynamicRepository>();

            _service = new ReportDateService(_cacheService, _options, _dynamicRepository);
        }

        [Fact]
        public async Task GetMoveBetsDay_ShouldReturnMoveBetsDayFromCache()
        {
            // Arrange
            var expected = new DateTime(2023, 1, 1);
            var balanceAndMoveBetsDay = new BalanceUpDayAndMoveBetsDay(new DateTime(2023, 1, 2), expected);

            _cacheService.GetOrSetAsync(
                Arg.Is("Alpha.Reports.BalanceUpDayAndMoveBetsDay"),
                    Arg.Any<Func<Task<BalanceUpDayAndMoveBetsDay>>>(),
                    Arg.Any<CacheItemOptions>())
                    .Returns(balanceAndMoveBetsDay);

            // Act
            var result = await _service.GetMoveBetsDay();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetBalanceUpDayAndMoveBetsDay_ShouldGetFromCacheWithCorrectParameters()
        {
            // Arrange
            var expected = new BalanceUpDayAndMoveBetsDay(new DateTime(2023, 1, 2), new DateTime(2023, 1, 1));

            _cacheService.GetOrSetAsync(
                Arg.Is("Alpha.Reports.BalanceUpDayAndMoveBetsDay"),
                Arg.Any<Func<Task<BalanceUpDayAndMoveBetsDay>>>(),
                Arg.Any<CacheItemOptions>())
                .Returns(expected);

            // Act
            var result = await _service.GetBalanceUpDayAndMoveBetsDay();

            // Assert
            Assert.Equal(expected, result);

            await _cacheService.Received(1).GetOrSetAsync(
                Arg.Is("Alpha.Reports.BalanceUpDayAndMoveBetsDay"),
                Arg.Any<Func<Task<BalanceUpDayAndMoveBetsDay>>>(),
                Arg.Any<CacheItemOptions>());
        }

        [Fact]
        public async Task GetBalanceUpDayAndMoveBetsDay_WhenCacheMisses_ShouldFetchFromRepository()
        {
            // Arrange
            var expected = new BalanceUpDayAndMoveBetsDay(new DateTime(2023, 1, 2), new DateTime(2023, 1, 1));

            _cacheService.GetOrSetAsync(
                Arg.Any<string>(),
                Arg.Any<Func<Task<BalanceUpDayAndMoveBetsDay>>>(),
                Arg.Any<CacheItemOptions>())
                .Returns(callInfo =>
                {
                    var factory = callInfo.ArgAt<Func<Task<BalanceUpDayAndMoveBetsDay>>>(1);
                    return factory();
                });

            _dynamicRepository.GetAsync<BalanceUpDayAndMoveBetsDay>(Arg.Any<GetBalanceUpDayAndMoveBetsDayCriteria>())
                .Returns(expected);

            // Act
            var result = await _service.GetBalanceUpDayAndMoveBetsDay();

            // Assert
            Assert.Equal(expected, result);
            await _dynamicRepository.Received(1).GetAsync<BalanceUpDayAndMoveBetsDay>(Arg.Any<GetBalanceUpDayAndMoveBetsDayCriteria>());
        }

        [Fact]
        public async Task GetLimitDate_ShouldGetFromCacheWithCorrectParameters()
        {
            // Arrange
            int custId = 123;
            DateTime expected = new DateTime(2023, 1, 15);

            _cacheService.GetOrSetAsync(
                Arg.Is($"Alpha.Reports.CustomerLimitDate.{custId}"),
                Arg.Any<Func<Task<DateTime>>>(),
                Arg.Any<CacheItemOptions>())
                .Returns(expected);

            // Act
            var result = await _service.GetLimitDate(custId);

            // Assert
            Assert.Equal(expected, result);

            await _cacheService.Received(1).GetOrSetAsync(
                Arg.Is($"Alpha.Reports.CustomerLimitDate.{custId}"),
                Arg.Any<Func<Task<DateTime>>>(),
                Arg.Any<CacheItemOptions>());
        }

        [Fact]
        public async Task GetLimitDate_WhenCustomerLimitDateFound_ShouldReturnDateLimit()
        {
            // Arrange
            int custId = 123;
            var expected = new DateTime(2023, 1, 15);
            var customerLimitDate = new CustomerLimitDate(expected);

            _cacheService.GetOrSetAsync(
                Arg.Any<string>(),
                Arg.Any<Func<Task<DateTime>>>(),
                Arg.Any<CacheItemOptions>())
                .Returns(callInfo =>
                {
                    var factory = callInfo.ArgAt<Func<Task<DateTime>>>(1);
                    return factory();
                });

            _dynamicRepository.GetAsync<CustomerLimitDate>(Arg.Any<GetLimitDateCriteria>())
                .Returns(customerLimitDate);

            // Act
            var result = await _service.GetLimitDate(custId);

            // Assert
            Assert.Equal(expected, result);
            await _dynamicRepository.Received(1).GetAsync<CustomerLimitDate>(
                Arg.Is<GetLimitDateCriteria>(c => c.CustId == custId));
        }

        [Fact]
        public async Task GetLimitDate_WhenCustomerLimitDateNotFound_ShouldReturnDateTimeMinValue()
        {
            // Arrange
            int custId = 456;

            _cacheService.GetOrSetAsync(
                Arg.Any<string>(),
                Arg.Any<Func<Task<DateTime>>>(),
                Arg.Any<CacheItemOptions>())
                .Returns(callInfo =>
                {
                    var factory = callInfo.ArgAt<Func<Task<DateTime>>>(1);
                    return factory();
                });

            _dynamicRepository.GetAsync<CustomerLimitDate>(Arg.Any<GetLimitDateCriteria>())
                .Returns(Task.FromResult<CustomerLimitDate>(null));

            // Act
            var result = await _service.GetLimitDate(custId);

            // Assert
            Assert.Equal(DateTime.MinValue, result);
        }
    }
}