using Alpha.API.Client.Models.Outstanding;

namespace Alpha.API.Client.Services;

public interface IOutstandingService
{
    Task<OutstandingReport> GetDownlineOutstanding(int custId, byte custLevel, byte downlineLevel, int downlineId, string siteLabel, string language = "en-US");

    Task<IEnumerable<string>> GetTopUsername(int custId, byte custLevel, int top = 5);

    Task<OutstandingReport> GetTopOutstanding(int custId, byte custLevel, int top = 50);
}

public class OutstandingService : ServiceBase, IOutstandingService
{
    public OutstandingService(HttpClient httpClient) : base(httpClient)
    {
    }

    public Task<OutstandingReport> GetDownlineOutstanding(int custId, byte custLevel, byte downlineLevel, int downlineId, string siteLabel, string language = "en-US")
        => Get<OutstandingReport>(
            $"/api/Outstanding/GetDownlineOutstanding?custId={custId}&custLevelId={custLevel}&downlineId={downlineId}&language={language}&sitelabel={siteLabel}&downlineLevelId={downlineLevel}");

    public Task<IEnumerable<string>> GetTopUsername(int custId, byte custLevel, int top = 5)
        => Get<IEnumerable<string>>(
            $"/api/Outstanding/GetTopUsername?custId={custId}&custLevelId={custLevel}&top={top}");

    public Task<OutstandingReport> GetTopOutstanding(int custId, byte custLevel, int top = 50)
        => Get<OutstandingReport>(
            $"/api/Outstanding/GetTopOutstanding?custId={custId}&custLevelId={custLevel}&top={top}");
}
