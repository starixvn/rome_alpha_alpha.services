using Alpha.Reports.Configurations;
using Alpha.Reports.Criteria.Outstanding;
using Alpha.Reports.Models.Outstanding;
using Alpha.Reports.Params;
using Alpha.Shared.Enumerations;
using Alpha.Shared.Extensions;
using Alpha.Shared.Services;
using Alpha.Shared.Utils;
using Fanex.Data.Repository;
using Microsoft.Extensions.Options;

namespace Alpha.Reports.Services;

public interface IOutstandingService
{
    Task<OutstandingReport> GetDownlineOutstanding(GetDownlineOutstandingParam param);

    Task<IEnumerable<string>> GetTopUsername(int custId, UserLevel custLevel, int top);

    Task<OutstandingReport> GetTopOutstanding(int custId, UserLevel custLevel, int top);

    Task<CustomerOutstanding> GetCustomerOutstanding(int custId);
}

public class OutstandingService : IOutstandingService
{
    private readonly IProductService productService;
    private readonly ICacheRefService cacheRefService;
    private readonly ReportAppSettings appSettings;
    private readonly IDynamicRepository dynamicRepository;

    public OutstandingService(
        IProductService productService,
        ICacheRefService cacheRefService,
        IOptions<ReportAppSettings> options,
        IDynamicRepository dynamicRepository)
    {
        this.productService = productService;
        this.cacheRefService = cacheRefService;
        appSettings = options.Value;
        this.dynamicRepository = dynamicRepository;
    }

    public async Task<OutstandingReport> GetDownlineOutstanding(GetDownlineOutstandingParam param)
    {
        var productIds = await productService.GetOfferedProducts(
            param.CustId,
            appSettings.SettingStatus,
            SystemTime.Today);
        var outstandings = await dynamicRepository.FetchAsync<OutstandingEntity>(
            new GetDownlineOutstandingCriteria(param.DownlineLevel, productIds.JoinWithComma(), param.DownlineId));

        return outstandings.Any()
            ? GetProductNameAndAccummulateTotal(param.CustLevel, outstandings, param.Language, param.SiteLabel)
            : new OutstandingReport();
    }

    private OutstandingReport GetProductNameAndAccummulateTotal(
        UserLevel custLevel,
        IEnumerable<OutstandingEntity> outstandingEntities,
        string language,
        string siteLabel)
    {
        OutstandingModel outstandingModel;
        var outstandings = new List<OutstandingModel>();
        var totalMemberOutStanding = 0M;
        var totalOutStanding = 0M;
        var totalToWin = 0M;
        var totalRiskAmount = 0M;
        foreach (var entity in outstandingEntities)
        {
            outstandingModel = GetOutstandingModel(
                custLevel,
                entity,
                cacheRefService.GetProductName(entity.Id, language, appSettings.Belonging777Group(siteLabel)));
            outstandings.Add(outstandingModel);
            totalToWin += outstandingModel.ToWin ?? 0;
            totalRiskAmount += outstandingModel.RiskAmount ?? 0;
            totalMemberOutStanding += outstandingModel.MemberOutstanding;
            totalOutStanding += outstandingModel.Outstanding;
        }

        var total = new OutstandingModel(0, "Total", totalMemberOutStanding, totalOutStanding, totalToWin, totalRiskAmount, false);

        return new OutstandingReport(outstandings, total);
    }

    public async Task<IEnumerable<string>> GetTopUsername(int custId, UserLevel custLevel, int top)
    {
        var productIds = (await productService.GetOfferedProducts(custId, appSettings.SettingStatus, SystemTime.Today)).JoinWithComma();
        var outstandings = await dynamicRepository.FetchAsync<OutstandingEntity>(
            new GetTopOutstandingCommand(custId, custLevel, productIds, top));

        return outstandings.Select(o => o.Name);
    }

    public async Task<OutstandingReport> GetTopOutstanding(int custId, UserLevel custLevel, int top)
    {
        var productIds = (await productService.GetOfferedProducts(custId, appSettings.SettingStatus, SystemTime.Today)).JoinWithComma();
        var command = new GetTopOutstandingCommand(custId, custLevel, productIds, top);
        var outstandings = await dynamicRepository.FetchAsync<OutstandingEntity>(command);

        return outstandings.Any()
            ? new OutstandingReport(
                outstandings.Select(entity => GetOutstandingModel(custLevel, entity)),
                GetTotalTopOutstanding(command))
            : new OutstandingReport();
    }

    private static OutstandingModel GetTotalTopOutstanding(GetTopOutstandingCommand command)
    {
        decimal totalOutStanding;
        decimal? totalRiskAmount;
        if (command.CustLevel.IsSuper())
        {
            totalOutStanding = command.TotalSuperOutstanding.Value;
            totalRiskAmount = command.TotalSuperRiskAmount;
        }
        else if (command.CustLevel.IsMaster())
        {
            totalOutStanding = command.TotalMasterOutstanding.Value;
            totalRiskAmount = command.TotalMasterRiskAmount;
        }
        else
        {
            totalOutStanding = command.TotalAgentOutstanding.Value;
            totalRiskAmount = command.TotalAgentRiskAmount;
        }

        return new OutstandingModel(0, "Total", command.TotalMemberOutstanding, totalOutStanding, command.TotalMemberRiskAmount, totalRiskAmount, false);
    }

    // Have to do manual mapping from OutstandingEntity --> OutstandingModel.
    // Cannot using Mapping attr of Fanex.Data because it has a bug caching or something.
    // https://nexdev.net/issues/173718
    private static OutstandingModel GetOutstandingModel(
        UserLevel custLevel,
        OutstandingEntity entity,
        string productName = null)
    {
        decimal outstandingValue;
        decimal? riskAmount;
        if (custLevel.IsSuper())
        {
            outstandingValue = entity.SuperOutstanding.Value;
            riskAmount = entity.SuperRisk;
        }
        else if (custLevel.IsMaster())
        {
            outstandingValue = entity.MasterOutstanding.Value;
            riskAmount = entity.MasterRisk;
        }
        else
        {
            outstandingValue = entity.AgentOutstanding.Value;
            riskAmount = entity.AgentRisk;
        }

        return new OutstandingModel(entity.Id, productName ?? entity.Name, entity.MemberOutstanding, outstandingValue, entity.ToWin, riskAmount, entity.IsDirectMember);
    }

    public Task<CustomerOutstanding> GetCustomerOutstanding(int custId)
        => dynamicRepository.GetAsync<CustomerOutstanding>(new GetCustomerOutstandingCriteria(custId));
}
