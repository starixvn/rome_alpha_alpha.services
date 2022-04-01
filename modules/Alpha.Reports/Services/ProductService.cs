using Alpha.Reports.Configurations;
using Alpha.Reports.Criteria;
using Alpha.Reports.Models;
using Fanex.Caching;
using Fanex.Data.Repository;
using Microsoft.Extensions.Options;

namespace Alpha.Reports.Services;

public interface IProductService
{
    Task<IEnumerable<int>> GetOfferedProducts(int custId, byte settingStatus, DateTime minReportDate);
}

public class ProductService : IProductService
{
    private const string ProductsCacheKey = "Alpha.Reports.Services.ProductService.OfferedProducts";

    private readonly ICacheService cacheService;
    private readonly ReportAppSettings appSettings;
    private readonly IDynamicRepository dynamicRepository;

    public ProductService(
        ICacheService cacheService,
        IOptions<ReportAppSettings> options,
        IDynamicRepository dynamicRepository)
    {
        this.cacheService = cacheService;
        appSettings = options.Value;
        this.dynamicRepository = dynamicRepository;
    }

    public async Task<IEnumerable<int>> GetOfferedProducts(int custId, byte settingStatus, DateTime minReportDate)
    {
        if (custId <= 0)
        {
            return Enumerable.Empty<int>();
        }

        var products = await GetOfferedProducts(custId, settingStatus);

        if (products == null)
        {
            return Enumerable.Empty<int>();
        }

        return products
            .Where(product => !product.IsHidden && IsRemainSupportProduct(product, minReportDate))
            .Select(product => product.ProductId);
    }

    private Task<IEnumerable<Product>> GetOfferedProducts(int custId, byte settingStatus)
        => cacheService.GetOrSetAsync<IEnumerable<Product>>(
            $"{ProductsCacheKey}.{custId}",
            () => dynamicRepository.FetchAsync<Product>(new GetOfferedProductsCriteria(custId, settingStatus)),
            new CacheItemOptions().SetAbsoluteExpiration(
                TimeSpan.FromSeconds(appSettings.OfferedProductsByCustIdCacheInSeconds)));

    private static bool IsRemainSupportProduct(Product product, DateTime minReportDate)
        => !product.EndOfSupportDate.HasValue
        || minReportDate <= product.EndOfSupportDate.Value;
}
