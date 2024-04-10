using Alpha.API.Requests.AutoTransfer;
using Alpha.Customers.Models;
using Alpha.Customers.Models.AutoTransfer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alpha.API.Controllers;

public class AutoTransferController : ControllerBaseApi
{
    public AutoTransferController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("IsAllDayBalanceTransfer")]
    public Task<bool> IsAllDayBalanceTransfer([FromBody]GetIsAllDayBalanceTransferQuery query)
        => ObjectAsync(query);

    [HttpPost("Monitor")]
    public Task<AutoTransferMonitoringPageModel> Monitor([FromBody]GetAutoTransferMonitorQuery query)
        => ObjectAsync(query);

    [HttpPost("Customers")]
    public Task<AutoTransferCustomersPageModel> Customers([FromBody]GetCustomersQuery query)
        => ObjectAsync(query);

    [HttpPost("UpdateStatus")]
    public Task<Result> UpdateStatus([FromBody]UpdateStatusQuery query)
        => ObjectAsync(query);

    [HttpPost("UpdateSchedule")]
    public Task<Result> UpdateSchedule([FromBody]UpdateScheduleQuery query)
        => ObjectAsync(query);

    [HttpPost("UpdateMultiple")]
    public Task<UpdateMultipleAutoTransferResultModel> UpdateMultiple([FromBody]UpdateMultipleQuery query)
        => ObjectAsync(query);
}
