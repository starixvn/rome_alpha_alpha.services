using System.Net;
using Alpha.API.Client.Models.Customers;

namespace Alpha.API.Client.Services;

public interface ICustomerService
{
    Task<IEnumerable<CustomerStatus>> GetCustomerStatusByBatch(IEnumerable<CustomerStatusParam> data);

    Task<CustomerStatus> GetCustomerStatus(int custId, int subAccId);

    Task<DownlineInfo> GetDownlineInfo(int custId, string downlineUsername);

    Task<IEnumerable<DomainInfo>> GetDomains(string site);
}

public class CustomerService : ServiceBase, ICustomerService
{
    public CustomerService(HttpClient httpClient) : base(httpClient)
    {
    }

    public Task<IEnumerable<CustomerStatus>> GetCustomerStatusByBatch(IEnumerable<CustomerStatusParam> data)
        => Post<IEnumerable<CustomerStatus>>($"/api/Customer/GetCustomerStatusByBatch", data);

    public Task<CustomerStatus> GetCustomerStatus(int custId, int subAccId)
        => Get<CustomerStatus>($"/api/Customer/GetCustomerStatus?custId={custId}&subAccId={subAccId}");

    public Task<DownlineInfo> GetDownlineInfo(int custId, string downlineUsername)
        => Get<DownlineInfo>(
            $"/api/Customer/GetDownlineInfo?custId={custId}&downlineUsername={WebUtility.UrlEncode(downlineUsername)}");

    public Task<IEnumerable<DomainInfo>> GetDomains(string site)
        => Get<IEnumerable<DomainInfo>>($"/api/Customer/GetDomains?site={site}");
}
