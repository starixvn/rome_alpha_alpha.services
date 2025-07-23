using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alpha.Reports.Configurations;
using Alpha.Reports.Models;
using Alpha.Reports.Services;
using Fanex.Caching;
using Fanex.Data.Repository;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace Alpha.Reports.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly ICacheService _cacheService;
        private readonly IOptions<ReportAppSettings> _options;
        private readonly IDynamicRepository _dynamicRepository;
        private readonly ProductService _sut;

        public ProductServiceTests()
        {
            _cacheService = Substitute.For<ICacheService>();
            _options = Substitute.For<IOptions<ReportAppSettings>>();
            _options.Value.Returns(new ReportAppSettings { OfferedProductsByCustIdCacheInSeconds = 60 });
            _dynamicRepository = Substitute.For<IDynamicRepository>();

            _sut = new ProductService(_cacheService, _options, _dynamicRepository);
        }

        [Fact]
        public async Task GetOfferedProducts_WithValidCustomerId_ReturnsFilteredProducts()
        {
            // Arrange
            const int custId = 123;
            const byte settingStatus = 1;
            var minReportDate = DateTime.Now;

            var products = new List<Product>
            {
                new Product(1, false, null),
                new Product(2, true, null),
                new Product(3, false, minReportDate.AddDays(1)),
                new Product(4, false, minReportDate.AddDays(-1))
            };

            _cacheService.GetOrSetAsync(
                    Arg.Is<string>(x => x == $"Alpha.Reports.OfferedProducts.{custId}"),
                    Arg.Any<Func<Task<IEnumerable<Product>>>>(),
                    Arg.Any<CacheItemOptions>())
                .Returns(Task.FromResult<IEnumerable<Product>>(products));

            // Act
            var result = await _sut.GetOfferedProducts(custId, settingStatus, minReportDate);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(1, result);
            Assert.Contains(3, result);
        }

        [Fact]
        public async Task GetOfferedProducts_WithInvalidCustomerId_ReturnsEmptyList()
        {
            // Arrange
            const int custId = 0;
            const byte settingStatus = 1;
            var minReportDate = DateTime.Now;

            // Act
            var result = await _sut.GetOfferedProducts(custId, settingStatus, minReportDate);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetOfferedProducts_WithNullProducts_ReturnsEmptyList()
        {
            // Arrange
            const int custId = 123;
            const byte settingStatus = 1;
            var minReportDate = DateTime.Now;

            _cacheService.GetOrSetAsync(
                    Arg.Is<string>(x => x == $"Alpha.Reports.OfferedProducts.{custId}"),
                    Arg.Any<Func<Task<IEnumerable<Product>>>>(),
                    Arg.Any<CacheItemOptions>())
                .Returns(Task.FromResult<IEnumerable<Product>>(null));

            // Act
            var result = await _sut.GetOfferedProducts(custId, settingStatus, minReportDate);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetOfferedProducts_WithCashServiceSites_ReturnsCombinedFilteredProducts()
        {
            // Arrange
            const int custId = 123;
            const byte settingStatus = 1;
            const string cashServiceSites = "site1,site2";
            var minReportDate = DateTime.Now;

            var products = new List<Product>
            {
                new Product(1, false, null),
                new Product(2, true, null)
            };

            var productsExtra = new List<Product>
            {
                new Product(3, false, minReportDate.AddDays(1)),
                new Product(4, true, null),
                new Product(5, false, minReportDate.AddDays(-1))
            };

            _cacheService.GetOrSetAsync(
                    Arg.Is<string>(x => x == $"Alpha.Reports.OfferedProducts.{custId}"),
                    Arg.Any<Func<Task<IEnumerable<Product>>>>(),
                    Arg.Any<CacheItemOptions>())
                .Returns(Task.FromResult<IEnumerable<Product>>(products));

            _cacheService.GetOrSetAsync(
                    Arg.Is<string>(x => x == $"Alpha.Reports.OfferedProductsExtra.{custId}"),
                    Arg.Any<Func<Task<IEnumerable<Product>>>>(),
                    Arg.Any<CacheItemOptions>())
                .Returns(Task.FromResult<IEnumerable<Product>>(productsExtra));

            // Act
            var result = await _sut.GetOfferedProducts(custId, settingStatus, minReportDate, cashServiceSites);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(1, result);
            Assert.Contains(3, result);
        }

        [Fact]
        public async Task GetOfferedProducts_WithEmptyCashServiceSites_ReturnsOnlyBaseProducts()
        {
            // Arrange
            const int custId = 123;
            const byte settingStatus = 1;
            const string cashServiceSites = "";
            var minReportDate = DateTime.Now;

            var products = new List<Product>
            {
                new Product(1, false, null),
                new Product(2, true, null)
            };

            _cacheService.GetOrSetAsync(
                    Arg.Is<string>(x => x == $"Alpha.Reports.OfferedProducts.{custId}"),
                Arg.Any<Func<Task<IEnumerable<Product>>>>(),
                Arg.Any<CacheItemOptions>())
            .Returns(Task.FromResult<IEnumerable<Product>>(products));

            // Act
            var result = await _sut.GetOfferedProducts(custId, settingStatus, minReportDate, cashServiceSites);

            // Assert
            Assert.Single(result);
            Assert.Contains(1, result);
        }
    }
}