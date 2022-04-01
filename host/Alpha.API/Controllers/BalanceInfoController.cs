using Alpha.API.Attributes;
using Alpha.API.Requests.Reports.BalanceInfo;
using Alpha.Reports.Models.BalanceInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alpha.API.Controllers;

//[TypeFilter(typeof(SubAccPermissionAuthorization))]
public class BalanceInfoController : ControllerBaseApi
{
    public BalanceInfoController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("GetMyBalance")]
    public Task<MyBalance> GetMyBalance([FromQuery]GetMyBalanceQuery query)
        => ObjectAsync(query);

    [HttpGet("GetTopUsername")]
    public Task<IEnumerable<string>> GetTopUsername([FromQuery]GetTopBalanceUsernameQuery query)
        => ObjectAsync(query);

    [HttpGet("GetTopBalance")]
    public Task<TopBalanceReport> GetTopBalance([FromQuery]GetTopBalanceQuery query)
        => ObjectAsync(query);
}
