using Alpha.API.Client.Models.TotalBets;

namespace Alpha.API.Client.Services;

public interface ITotalBetsService
{
    Task<TotalBetsModel> Summary(int custId, int subAccId, int customerLevel, string language, bool isApplyPT);
    Task<TotalBetsDetailsModel> Details(int custId, int subAccId, int customerLevel, string language, int matchId, bool isApplyPT);
}

public class TotalBetsService : ServiceBase, ITotalBetsService
{
    public TotalBetsService(HttpClient httpClient) : base(httpClient)
    {
    }

    public Task<TotalBetsModel> Summary(int custId, int subAccId, int customerLevel, string language, bool isApplyPT)
        => Get<TotalBetsModel>(
            $"/api/TotalBets/Summary?custId={custId}&custLevelId={customerLevel}&isApplyPT={isApplyPT}&language={language}");

    public Task<TotalBetsDetailsModel> Details(int custId, int subAccId, int customerLevel, string language, int matchId, bool isApplyPT)
        => Get<TotalBetsDetailsModel>(
            $"/api/TotalBets/Details?custId={custId}&custLevelId={customerLevel}&isApplyPT={isApplyPT}&language={language}&matchId={matchId}");
}