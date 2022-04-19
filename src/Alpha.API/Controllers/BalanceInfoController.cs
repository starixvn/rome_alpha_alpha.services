using Alpha.API.Requests.Reports.BalanceInfo;
using Alpha.Reports.Models.BalanceInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alpha.API.Controllers;

public class BalanceInfoController : ControllerBaseApi
{
    public BalanceInfoController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("GetCustomerBalance")]
    public Task<CustomerBalance> GetCustomerBalance([FromQuery]GetCustomerBalanceQuery query)
        => ObjectAsync(query);

    [HttpGet("GetDownlineBalance")]
    public Task<DownlineBalance> GetDownlineBalance([FromQuery]GetDownlineBalanceQuery query)
        => ObjectAsync(query);

    [HttpGet("GetTopUsername")]
    public Task<IEnumerable<string>> GetTopUsername([FromQuery]GetTopBalanceUsernameQuery query)
        => ObjectAsync(query);

    [HttpGet("GetTopBalance")]
    public Task<IEnumerable<TopBalance>> GetTopBalance([FromQuery]GetTopBalanceQuery query)
        => ObjectAsync(query);
}
