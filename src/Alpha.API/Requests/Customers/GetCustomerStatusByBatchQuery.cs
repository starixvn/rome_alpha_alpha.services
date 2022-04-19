using Alpha.Customers.Models;
using Alpha.Customers.Params;
using MediatR;

namespace Alpha.API.Requests.Customers;

public record GetCustomerStatusByBatchQuery(IEnumerable<CustomerStatusParam> CustomerStatus)
    : IRequest<IEnumerable<CustomerStatus>>;
