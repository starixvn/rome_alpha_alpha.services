using Alpha.API.Attributes;
using Alpha.API.Requests.Reports.WinLoss;
using Alpha.Reports.Models.WinLoss;
using Alpha.Shared.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alpha.API.Controllers;

//[TypeFilter(typeof(SubAccPermissionAuthorization))]
public class WinLossController : ControllerBaseApi
{
    public WinLossController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("GetTodayWinLoss")]
    public Task<WinLossBase> GetTodayWinLoss(int custId, int subAccId, byte custLevel)
        => ObjectAsync(new GetWinLossQuery(custId, subAccId, custLevel, SystemTime.Today));

    [HttpGet("GetYesterdayWinLoss")]
    public Task<WinLossBase> GetYesterdayWinLoss(int custId, int subAccId, byte custLevel)
        => ObjectAsync(new GetWinLossQuery(custId, subAccId, custLevel, SystemTime.Yesterday));
}
