using Alpha.Customers.Configurations;
using Alpha.Customers.Criteria;
using Alpha.Customers.Models;
using Fanex.Caching;
using Fanex.Data.Repository;
using Microsoft.Extensions.Options;

namespace Alpha.Customers.Services;

public interface ICustomerService
{
    Task<CustomerInfo> GetCustomerInfoAsync(int custId);

    Task<CustomerStatus> GetCustomerStatusAsync(int custId, int subAccountId);

    Task<DownlineInfo> GetDownlineInfoAsync(int custId, string downlineUsername);

    Task<bool> IsSubAccHasReportPermissionAsync(int custId, int subAccId);
}

public class CustomerService : ICustomerService
{
    private const string CustomerInfoCacheKey = "Alpha.Customers.Services.CustomerInfo";
    private const string CustomerStatusCacheKey = "Alpha.Customers.Services.CustomerStatus";
    private const string ReportPermission = "[E]";
    private const string SubAccPermissionCacheKey = "Alpha.Customers.Services.SubAccPermission";

    private readonly ICacheService cacheService;
    private readonly CustomerAppSettings appSettings;
    private readonly IDynamicRepository dynamicRepository;

    public CustomerService(
        ICacheService cacheService,
        IOptions<CustomerAppSettings> options,
        IDynamicRepository dynamicRepository)
    {
        this.cacheService = cacheService;
        appSettings = options.Value;
        this.dynamicRepository = dynamicRepository;
    }

    public Task<CustomerInfo> GetCustomerInfoAsync(int custId)
        => cacheService.GetOrSetAsync<CustomerInfo>(
            $"{CustomerInfoCacheKey}.{custId}",
            () => GetCustomerInfoFromDBAsync(custId),
            new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(appSettings.CustomerInfoCacheInSeconds)));

    private async Task<CustomerInfo> GetCustomerInfoFromDBAsync(int custId)
    {
        var customerInfo = await dynamicRepository.GetAsync<CustomerInfo>(new GetCustomerInfoCriteria(custId));
        
        return customerInfo ?? new CustomerInfo();
    }
    

    public Task<CustomerStatus> GetCustomerStatusAsync(int custId, int subAccountId)
        => cacheService.GetOrSetAsync(
            $"{CustomerStatusCacheKey}.{custId}.{subAccountId}",
            () => GetCustomerStatusWithoutCacheAsync(custId, subAccountId),
            new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(appSettings.CustomerStatusCacheInSeconds)));

    private async Task<CustomerStatus> GetCustomerStatusWithoutCacheAsync(int custId, int subAccountId)
    {
        var customerStatus = await GetCustomerStatusFromDBAsync(custId, subAccountId);
        var customerInfo = await GetCustomerInfoAsync(custId);

        if (customerInfo != null)
        {
            return new CustomerStatus(
                customerStatus.IsActive,
                customerStatus.Locked,
                customerInfo.IsProtectedMode);
        }

        return customerStatus;
    }

    private async Task<CustomerStatus> GetCustomerStatusFromDBAsync(int custId, int subAccountId)
    {
        var command = new GetCustomerStatusCommand(custId, subAccountId);
        await dynamicRepository.ExecuteAsync(command);

        return new CustomerStatus(command.IsActive, command.Locked);
    }

    public async Task<DownlineInfo> GetDownlineInfoAsync(int custId, string downlineUsername)
    {
        var command = new GetDownlineInfoCommand(custId, 0, downlineUsername);
        await dynamicRepository.ExecuteAsync(command);

        return new DownlineInfo(command.CustId, (byte)command.RoleId, command.IsSubAcc);
    }

    public async Task<bool> IsSubAccHasReportPermissionAsync(int custId, int subAccId)
        => (await GetSubAccPermissionAsync(custId, subAccId)).Contains(ReportPermission);

    private Task<string> GetSubAccPermissionAsync(int custId, int subAccId)
        => cacheService.GetOrSetAsync<string>(
            $"{SubAccPermissionCacheKey}.{custId}.{subAccId}",
            () => GetSubAccPermissionFromDBAsync(custId, subAccId),
            new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(appSettings.SubAccPermissionCacheInSeconds)));

    private async Task<string> GetSubAccPermissionFromDBAsync(int custId, int subAccId)
    {
        var subAccInfo = await dynamicRepository.GetAsync<SubAccInfo>(new GetSubAccInfoCriteria(custId, subAccId));
        
        return subAccInfo == null
            ? string.Empty
            : subAccInfo.PageAccess;
    }
}
