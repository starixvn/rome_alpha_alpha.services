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
            new GetDownlineOutstandingCriteria(param.CustLevel, productIds.JoinWithComma(), param.DownlineId));
        
        OutstandingEntity total = outstandings.Any()
            ? GetProductNameAndAccummulateTotal(outstandings, param.Language)
            : null;

        return new OutstandingReport(outstandings, total);
    }

    private OutstandingEntity GetProductNameAndAccummulateTotal(
        IEnumerable<OutstandingEntity> outstandings,
        string language)
    {
        var totalMemberOutStanding = 0M;
        var totalOutStanding = 0M;

        foreach (var item in outstandings)
        {
            item.Name = cacheRefService.GetProductName(item.Id, language);
            totalMemberOutStanding += item.MemberOutstanding;
            totalOutStanding += item.Outstanding;
        }

        return new OutstandingEntity(0, "Total", totalMemberOutStanding, totalOutStanding);
    }

    public async Task<IEnumerable<string>> GetTopUsername(int custId, UserLevel custLevel, int top)
        => (await GetTopOutstanding(custId, custLevel, top)).Outstandings.Select(o => o.Name);

    public async Task<OutstandingReport> GetTopOutstanding(int custId, UserLevel custLevel, int top)
    {
        var productIds = (await productService.GetOfferedProducts(custId, appSettings.SettingStatus, SystemTime.Today)).JoinWithComma();
        
        if (custLevel.IsSuper())
        {
            return await GetTopMasterOutstanding(custId, productIds, top);
        }

        if (custLevel.IsMaster())
        {
            return await GetTopAgentOutstanding(custId, productIds, top);
        }

        return await GetTopMemberOutstanding(custId, productIds, top);
    }

    private async Task<OutstandingReport> GetTopMasterOutstanding(int custId, string productIds, int top)
    {
        var command = new GetTopMasterOutstandingCriteria(custId, productIds, top);
        var outstandings = await dynamicRepository.FetchAsync<OutstandingEntity>(command);
        var total = new OutstandingEntity(0, "Total", command.TotalMemberOutstanding, command.TotalSuperOutstanding);

        return new OutstandingReport(outstandings, total);
    }

    private async Task<OutstandingReport> GetTopAgentOutstanding(int custId, string productIds, int top)
    {
        var command = new GetTopAgentOutstandingCriteria(custId, productIds, top);
        var outstandings = await dynamicRepository.FetchAsync<OutstandingEntity>(command);
        var total = new OutstandingEntity(0, "Total", command.TotalMemberOutstanding, command.TotalMasterOutstanding);

        return new OutstandingReport(outstandings, total);
    }

    private async Task<OutstandingReport> GetTopMemberOutstanding(int custId, string productIds, int top)
    {
        var command = new GetTopMemberOutstandingCriteria(custId, productIds, top);
        var outstandings = await dynamicRepository.FetchAsync<OutstandingEntity>(command);
        var total = new OutstandingEntity(0, "Total", command.TotalMemberOutstanding, command.TotalAgentOutstanding);

        return new OutstandingReport(outstandings, total);
    }

    public Task<CustomerOutstanding> GetCustomerOutstanding(int custId)
        => dynamicRepository.GetAsync<CustomerOutstanding>(new GetCustomerOutstandingCriteria(custId));
}
