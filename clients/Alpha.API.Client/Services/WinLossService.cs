using Alpha.API.Client.Constants;
using Alpha.API.Client.Models.WinLoss;

namespace Alpha.API.Client.Services;

public interface IWinLossService
{
    Task<WinLossBase> GetWinLoss(int custId, byte custLevel, DateTime date);

    Task<WinLossBase> GetDownlineWinLoss(int custId, byte custLevel, DateTime date, int downlineId);

    Task<IEnumerable<string>> GetTopUsername(int custId, byte custLevel, DateTime date, int top = 5);
}

public class WinLossService : ServiceBase, IWinLossService
{
    public WinLossService(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<WinLossBase> GetWinLoss(int custId, byte custLevel, DateTime date)
    {
        var url = $"/api/WinLoss/GetWinLoss?custId={custId}&custLevelId={custLevel}&date={date}";

        if (custLevel == CustLevel.Super)
        {
            return await Get<WinLossMaster>(url);
        }

        if (custLevel == CustLevel.Master)
        {
            return await Get<WinLossAgent>(url);
        }

        return await Get<WinLossMember>(url);
    }
    
    public async Task<WinLossBase> GetDownlineWinLoss(int custId, byte custLevel, DateTime date, int downlineId)
    {
        var directDownlineLevel = custLevel - 1;
        var url = $"/api/WinLoss/GetDownlineWinLoss?custId={custId}&custLevelId={custLevel}&date={date}&downlineId={downlineId}";
        
        if (directDownlineLevel == CustLevel.Master)
        {
            return await Get<WinLossAgent>(url);
        }

        return await Get<WinLossMember>(url);
    }

    public Task<IEnumerable<string>> GetTopUsername(int custId, byte custLevel, DateTime date, int top = 5)
        => Get<IEnumerable<string>>($"/api/WinLoss/GetTopUsername?custId={custId}&custLevelId={custLevel}&date={date}&top={top}");
}