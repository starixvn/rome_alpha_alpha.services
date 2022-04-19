using Alpha.API.Requests.Customers;
using Alpha.Customers.Models;
using Alpha.Customers.Params;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alpha.API.Controllers;

public class CustomerController : ControllerBaseApi
{
    public CustomerController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("GetCustomerStatusByBatch")]
    public Task<IEnumerable<CustomerStatus>> GetCustomerStatusByBatch([FromBody]IEnumerable<CustomerStatusParam> query)
        => ObjectAsync(new GetCustomerStatusByBatchQuery(query));

    [HttpGet("GetCustomerStatus")]
    public Task<CustomerStatus> GetCustomerStatus([FromQuery]GetCustomerStatusQuery query)
        => ObjectAsync(query);

    [HttpGet("GetDownlineInfo")]
    public Task<DownlineInfo> GetDownlineInfo([FromQuery]GetDownlineInfoQuery query)
        => ObjectAsync(query);
}
