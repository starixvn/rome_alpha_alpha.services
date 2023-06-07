using Alpha.API.Client.Models.BalanceInfo;

namespace Alpha.API.Client.Services;

public interface IBalanceInfoService
{
    Task<CustomerBalance> GetMyBalance(int custId, byte custLevel);

    Task<DownlineBalance> GetDownlineBalance(int custId, byte custLevel, string siteDBGroupIp);

    Task<IEnumerable<string>> GetTopUsername(int custId, byte custLevel, int top = 5);

    Task<IEnumerable<TopBalance>> GetTopBalance(int custId, byte custLevel, int top = 50);
}

public class BalanceInfoService : ServiceBase, IBalanceInfoService
{
    public BalanceInfoService(HttpClient httpClient) : base(httpClient)
    {
    }

    public Task<CustomerBalance> GetMyBalance(int custId, byte custLevel)
        => Get<CustomerBalance>(
            $"/api/BalanceInfo/GetCustomerBalance?custId={custId}&custLevelId={custLevel}");

    public Task<DownlineBalance> GetDownlineBalance(int custId, byte custLevel, string siteDBGroupIp)
        => Get<DownlineBalance>(
            $"/api/BalanceInfo/GetDownlineBalance?custId={custId}&custLevelId={custLevel}&SiteDBServer={siteDBGroupIp}");

    public Task<IEnumerable<string>> GetTopUsername(int custId, byte custLevel, int top = 5)
        => Get<IEnumerable<string>>(
            $"/api/BalanceInfo/GetTopUsername?custId={custId}&custLevelId={custLevel}&top={top}");

    public Task<IEnumerable<TopBalance>> GetTopBalance(int custId, byte custLevel, int top = 50)
        => Get<IEnumerable<TopBalance>>(
            $"/api/BalanceInfo/GetTopBalance?custId={custId}&custLevelId={custLevel}&top={top}");
}