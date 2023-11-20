using Alpha.API.Requests.Customers;
using Alpha.Customers.Models;
using Alpha.Customers.Params;
using Alpha.Customers.Services;
using MediatR;

namespace Alpha.API.Handlers;

public class CustomerHandler :
    IRequestHandler<GetCustomerStatusByBatchQuery, IEnumerable<CustomerStatus>>,
    IRequestHandler<GetCustomerStatusQuery, CustomerStatus>,
    IRequestHandler<GetDownlineInfoQuery, DownlineInfo>,
    IRequestHandler<GetDomainsQuery, IEnumerable<DomainInfo>>
{
    private readonly ICustomerService customerService;

    public CustomerHandler(ICustomerService customerService)
        => this.customerService = customerService;

    public Task<IEnumerable<CustomerStatus>> Handle(GetCustomerStatusByBatchQuery request, CancellationToken cancellationToken)
        => customerService.GetCustomerStatus(request.CustomerStatus);

    public Task<CustomerStatus> Handle(GetCustomerStatusQuery query, CancellationToken cancellationToken)
        => customerService.GetCustomerStatus(new CustomerStatusParam(query.CustId, query.SubAccId));

    public Task<DownlineInfo> Handle(GetDownlineInfoQuery query, CancellationToken cancellationToken)
        => customerService.GetDownlineInfo(query.CustId, query.DownlineUsername);

    public Task<IEnumerable<DomainInfo>> Handle(GetDomainsQuery query, CancellationToken cancellationToken)
        => customerService.GetDomains(query.Site);
}
