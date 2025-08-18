using Alpha.Reports.Configurations;
using Alpha.Reports.Criteria;
using Alpha.Reports.Models;
using Alpha.Shared.Exceptions;
using Alpha.Shared.Extensions;
using Fanex.Data.Repository;
using Microsoft.Extensions.Options;

namespace Alpha.Reports.Services;

public interface IMinimizeUMService
{
    Task ThrowIfUM(string controllerName, int custId);
}

public class MinimizeUMService : IMinimizeUMService
{
    private readonly MinimizeUM[] minimizeUMArray;
    private readonly IDynamicRepository dynamicRepository;

    public MinimizeUMService(
        IOptions<ReportAppSettings> options,
        IDynamicRepository dynamicRepository)
    {
        minimizeUMArray = options.Value.MinimizeUM;
        this.dynamicRepository = dynamicRepository;
    }

    public async Task ThrowIfUM(string controllerName, int custId)
    {
        var minimizeUM = Array.Find(
            minimizeUMArray,
            u => u.Controllers != null && Array.Exists(
                u.Controllers,
                c => c.Equals(controllerName, StringComparison.OrdinalIgnoreCase))
        );

        if (minimizeUM == null)
        {
            return;
        }

        var umInfoList = await dynamicRepository.FetchAsync<UMInfo>(new GetUMInfoCriteria(custId));

        if (!umInfoList.Any())
        {
            return;
        }

        var umInfo = umInfoList.FirstOrDefault(u => u.FunctionTypeName.Equals(minimizeUM.FunctionName, StringComparison.OrdinalIgnoreCase));

        if (umInfo != null)
        {
            throw new MinimizeUMException(umInfo.BeginTime.ToLongDateTimeMinute(), umInfo.EndTime.ToLongDateTimeMinute());
        }
    }
}