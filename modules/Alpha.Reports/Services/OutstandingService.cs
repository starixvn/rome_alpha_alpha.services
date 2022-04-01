using Alpha.Reports.Configurations;
using Alpha.Reports.Criteria.Outstanding;
using Alpha.Reports.Models.Outstanding;
using Alpha.Reports.Params.Outstanding;
using Alpha.Shared.Enumerations;
using Alpha.Shared.Extensions;
using Alpha.Shared.Services;
using Alpha.Shared.Utils;
using Fanex.Data.Repository;
using Microsoft.Extensions.Options;

namespace Alpha.Reports.Services;

public interface IOutstandingService
{
    Task<MyOutstanding> GetMyOutstandingAsync(int custId, UserLevel custLevel);

    Task<IEnumerable<DownlineOutstanding>> GetDownlineOutstandingAsync(GetDownlineOutstandingParam param);

    Task<IEnumerable<string>> GetTopUsernameAsync(int custId, UserLevel custLevel, int top);

    Task<TopOutstanding> GetTopOutstandingAsync(int custId, UserLevel custLevel, int top);
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

    public Task<MyOutstanding> GetMyOutstandingAsync(int custId, UserLevel custLevel)
        => dynamicRepository.GetAsync<MyOutstanding>(new GetMyOutstandingCriteria(custId, custLevel));

    public async Task<IEnumerable<DownlineOutstanding>> GetDownlineOutstandingAsync(GetDownlineOutstandingParam param)
    {
        var productIds = await productService.GetOfferedProducts(
            param.CustId,
            appSettings.SettingStatus,
            SystemTime.Today);
        var outstandings = await dynamicRepository.FetchAsync<DownlineOutstanding>(
            new GetDownlineOutstandingCriteria(param.CustLevel, productIds.JoinWithComma(), param.DownlineId));

        foreach (var item in outstandings)
        {
            item.Name = cacheRefService.GetProductName(item.Id, param.Language);
        }

        return outstandings;
    }

    public async Task<IEnumerable<string>> GetTopUsernameAsync(int custId, UserLevel custLevel, int top)
    {
        var outstandings = await GetTopOutstandingAsync(custId, custLevel);

        return outstandings.Take(top).Select(o => o.Name);
    }

    public async Task<TopOutstanding> GetTopOutstandingAsync(int custId, UserLevel custLevel, int top)
    {
        var outstandings = await GetTopOutstandingAsync(custId, custLevel);
        var totalOutstandings = outstandings.Count();

        if (totalOutstandings == 0)
        {
            return new TopOutstanding();
        }

        var hiddenDownlinesCount = totalOutstandings - top;

        if (top > totalOutstandings)
        {
            top = totalOutstandings;
            hiddenDownlinesCount = 0;
        }

        var (topOutstandings, total) = GetTopAndAccummulateTotal(outstandings, top);

        return new TopOutstanding(topOutstandings, total, hiddenDownlinesCount);
    }

    private async Task<IEnumerable<DownlineOutstanding>> GetTopOutstandingAsync(int custId, UserLevel custLevel)
    {
        var productIds = await productService.GetOfferedProducts(custId, appSettings.SettingStatus, SystemTime.Today);
        var outstandings = await dynamicRepository.FetchAsync<DownlineOutstanding>(
            new GetTopOutstandingCriteria(custId, custLevel, productIds.JoinWithComma()));

        return outstandings.OrderByDescending(o => o.MemberOutstanding);
    }

    private static Tuple<IEnumerable<DownlineOutstanding>, DownlineOutstanding> GetTopAndAccummulateTotal(IEnumerable<DownlineOutstanding> outstandings, int top)
    {
        var index = 0;
        var topOutstandings = new List<DownlineOutstanding>();
        var totalMemberOutStanding = 0M;
        var totalOutStanding = 0M;

        outstandings
            .ToList()
            .ForEach(item =>
            {
                if (index < top)
                {
                    topOutstandings.Add(item);
                    index++;
                }

                totalMemberOutStanding += item.MemberOutstanding;
                totalOutStanding += item.Outstanding;
            });

        return new Tuple<IEnumerable<DownlineOutstanding>, DownlineOutstanding>(
            topOutstandings,
            new DownlineOutstanding(0, "Total", totalMemberOutStanding, totalOutStanding));
    }
}
