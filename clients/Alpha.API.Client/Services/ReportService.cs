using Alpha.API.Client.Models.Outstanding;

namespace Alpha.API.Client.Services;

public interface IReportService
{
    Task<MyOutstanding> GetMyOutstandingAsync(int custId, int subAccId, byte custLevel);

    Task<IEnumerable<DownlineOutstanding>> GetDownlineOutstandingAsync(int custId, int subAccId, byte custLevel, int downlineId, string language = "en-US");

    Task<IEnumerable<string>> GetTopOutstandingUsernameAsync(int custId, int subAccId, byte custLevel, int top = 5);

    Task<TopOutstanding> GetTopOutstandingAsync(int custId, int subAccId, byte custLevel, int top);
}

public class ReportService : ServiceBase, IReportService
{
    public ReportService(HttpClient httpClient) : base(httpClient)
    {
    }

    public Task<MyOutstanding> GetMyOutstandingAsync(int custId, int subAccId, byte custLevel)
        => GetAsync<MyOutstanding>(
            $"/api/Outstanding/GetMyOutstanding?custId={custId}&subAccId={subAccId}&custLevelId={custLevel}");

    public Task<IEnumerable<DownlineOutstanding>> GetDownlineOutstandingAsync(int custId, int subAccId, byte custLevel, int downlineId, string language = "en-US")
        => GetAsync<IEnumerable<DownlineOutstanding>>(
            $"/api/Outstanding/GetDownlineOutstanding?custId={custId}&subAccId={subAccId}&custLevelId={custLevel}&downlineId={downlineId}&language={language}");

    public Task<IEnumerable<string>> GetTopOutstandingUsernameAsync(int custId, int subAccId, byte custLevel, int top = 5)
        => GetAsync<IEnumerable<string>>(
            $"/api/Outstanding/GetTopUsername?custId={custId}&subAccId={subAccId}&custLevelId={custLevel}&top={top}");

    public Task<TopOutstanding> GetTopOutstandingAsync(int custId, int subAccId, byte custLevel, int top)
        => GetAsync<TopOutstanding>(
            $"/api/Outstanding/GetTopOutstanding?custId={custId}&subAccId={subAccId}&custLevelId={custLevel}&top={top}");
}
