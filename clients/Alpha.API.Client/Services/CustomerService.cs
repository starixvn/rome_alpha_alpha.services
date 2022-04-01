using Alpha.API.Client.Models.Customers;

namespace Alpha.API.Client.Services;

public interface ICustomerService
{
    Task<CustomerStatus> GetCustomerStatusAsync(int custId, int subAccId);
    
    Task<DownlineInfo> GetDownlineInfoAsync(int custId, string downlineUsername);

    Task<bool> IsSubAccHasReportPermissionAsync(int custId, int subAccId);
}

public class CustomerService : ServiceBase, ICustomerService
{
    public CustomerService(HttpClient httpClient) : base(httpClient)
    {
    }

    public Task<CustomerStatus> GetCustomerStatusAsync(int custId, int subAccId)
        => GetAsync<CustomerStatus>($"/api/Customer/GetCustomerStatus?custId={custId}&subAccId={subAccId}");

    public Task<DownlineInfo> GetDownlineInfoAsync(int custId, string downlineUsername)
        => GetAsync<DownlineInfo>(
            $"/api/Customer/GetDownlineInfo?custId={custId}&downlineUsername={downlineUsername}");

    public Task<bool> IsSubAccHasReportPermissionAsync(int custId, int subAccId)
        => GetAsync<bool>($"/api/Customer/IsSubAccHasReportPermission?custId={custId}&subAccId={subAccId}");
}
