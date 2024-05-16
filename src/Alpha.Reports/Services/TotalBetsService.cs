using Alpha.Reports.Consts;
using Alpha.Reports.Criteria.BalanceInfo;
using Alpha.Reports.Models.TotalBets;
using Alpha.Shared.Services;
using Fanex.Data.Repository;

namespace Alpha.Reports.Services;

public interface ITotalBetsService
{
    Task<TotalBetsModel> Summary(int custId, bool isApplyPT, string language);
    Task<TotalBetsDetailsModel> Details(int custId, long matchId, bool isApplyPT, string language);
}

public class TotalBetsService : ITotalBetsService
{
    private readonly ICacheRefService cacheRefService;
    private readonly IDynamicRepository dynamicRepository;

    public TotalBetsService(ICacheRefService cacheRefService, IDynamicRepository dynamicRepository)
    {
        this.cacheRefService = cacheRefService;
        this.dynamicRepository = dynamicRepository;
    }

     public async Task<TotalBetsModel> Summary(int custId, bool isApplyPT, string language) {
        var criteria = new GetTotalBetsCriteria(custId, isApplyPT);
        var data = await  dynamicRepository.FetchAsync<MatchModel>(criteria);
        var webLang = LanguageCodes.MapWebAppLanguage(language);

        foreach(var d in data) {
            d.HomeName = cacheRefService.GetTeamName(d.HomeId, webLang);
            d.AwayName = cacheRefService.GetTeamName(d.AwayId, webLang);
        }

        return new TotalBetsModel{
            Matches = data
        };
      }

      public async Task<TotalBetsDetailsModel> Details(int custId, long matchId, bool isApplyPT, string language) {
        var criteria = new GetTotalBetsDetailsCriteria(custId, matchId, isApplyPT);
        var webLang = LanguageCodes.MapWebAppLanguage(language);

		var (matches, betTypes, toBetTypes) = await dynamicRepository.FetchMultipleAsync<MatchModel, BetTypeModel, BetTypeModel>(criteria).ConfigureAwait(false);

        foreach(var d in matches) {
            d.LeagueName = cacheRefService.GetLeagueName(d.LeagueId, webLang);
            d.HomeName = cacheRefService.GetTeamName(d.HomeId, webLang);
            d.AwayName = cacheRefService.GetTeamName(d.AwayId, webLang);
        }

        foreach(var d in betTypes) {
            d.BetTypeName = d.BetTypeId == 0 ? "Others" : cacheRefService.GetBetTypeName(d.BetTypeId, webLang);
        }

        foreach(var d in toBetTypes) {
            d.HDP = decimal.Parse(d.HDP).ToString("0.00");
            d.BetTypeName = d.BetTypeId == 0 ? "Others" : cacheRefService.GetBetTypeName(d.BetTypeId, webLang);
        }

        return new TotalBetsDetailsModel {
            Match = matches.FirstOrDefault(),
            BetTypes = betTypes,
            TOBetTypes = toBetTypes
        };
      }
}
