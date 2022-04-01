using Alpha.API.Requests.Customers;
using Alpha.Customers.Models;
using Alpha.Customers.Services;
using MediatR;

namespace Alpha.API.Handlers;

public class CustomerHandler :
    IRequestHandler<GetCustomerInfoQuery, CustomerInfo>,
    IRequestHandler<GetCustomerStatusQuery, CustomerStatus>,
    IRequestHandler<GetDownlineInfoQuery, DownlineInfo>,
    IRequestHandler<CheckSubAccReportPermissonQuery, bool>
{
    private readonly ICustomerService customerService;

    public CustomerHandler(ICustomerService customerService)
        => this.customerService = customerService;
    
    public Task<CustomerInfo> Handle(GetCustomerInfoQuery query, CancellationToken cancellationToken)
        => customerService.GetCustomerInfoAsync(query.CustId);

    public Task<CustomerStatus> Handle(GetCustomerStatusQuery query, CancellationToken cancellationToken)
        => customerService.GetCustomerStatusAsync(query.CustId, query.SubAccId);

    public Task<DownlineInfo> Handle(GetDownlineInfoQuery query, CancellationToken cancellationToken)
        => customerService.GetDownlineInfoAsync(query.CustId, query.DownlineUsername);

    public Task<bool> Handle(CheckSubAccReportPermissonQuery query, CancellationToken cancellationToken)
        => customerService.IsSubAccHasReportPermissionAsync(query.CustId, query.SubAccId);
}
