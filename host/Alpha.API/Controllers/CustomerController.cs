using Alpha.API.Requests.Customers;
using Alpha.Customers.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alpha.API.Controllers;

public class CustomerController : ControllerBaseApi
{
    public CustomerController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("GetCustomerInfo")]
    public Task<CustomerInfo> GetCustomerInfo([FromQuery]GetCustomerInfoQuery query)
        => ObjectAsync(query);

    [HttpGet("GetCustomerStatus")]
    public Task<CustomerStatus> GetCustomerStatus([FromQuery]GetCustomerStatusQuery query)
        => ObjectAsync(query);

    [HttpGet("GetDownlineInfo")]
    public Task<DownlineInfo> GetDownlineInfo([FromQuery]GetDownlineInfoQuery query)
        => ObjectAsync(query);

    [HttpGet("IsSubAccHasReportPermission")]
    public Task<bool> IsSubAccHasReportPermission([FromQuery]CheckSubAccReportPermissonQuery query)
        => ObjectAsync(query);
}
