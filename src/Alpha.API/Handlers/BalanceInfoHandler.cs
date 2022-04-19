using Alpha.API.Requests.Reports.BalanceInfo;
using Alpha.Reports.Models.BalanceInfo;
using Alpha.Reports.Services;
using MediatR;

namespace Alpha.API.Handlers;

public class BalanceInfoHandler :
    IRequestHandler<GetCustomerBalanceQuery, CustomerBalance>,
    IRequestHandler<GetDownlineBalanceQuery, DownlineBalance>,
    IRequestHandler<GetTopBalanceUsernameQuery, IEnumerable<string>>,
    IRequestHandler<GetTopBalanceQuery, IEnumerable<TopBalance>>
{
    private readonly IBalanceInfoService balanceInfoService;

    public BalanceInfoHandler(IBalanceInfoService balanceInfoService)
        => this.balanceInfoService = balanceInfoService;

    public Task<CustomerBalance> Handle(GetCustomerBalanceQuery query, CancellationToken cancellationToken)
        => balanceInfoService.GetCustomerBalance(query.CustId, query.CustLevel);

    public Task<DownlineBalance> Handle(GetDownlineBalanceQuery query, CancellationToken cancellationToken)
        => balanceInfoService.GetDownlineBalance(query.CustId, query.CustLevel, query.DownlineId);

    public Task<IEnumerable<string>> Handle(GetTopBalanceUsernameQuery query, CancellationToken cancellationToken)
        => balanceInfoService.GetTopUsername(query.CustId, query.CustLevel, query.Top);

    public Task<IEnumerable<TopBalance>> Handle(GetTopBalanceQuery query, CancellationToken cancellationToken)
        => balanceInfoService.GetTopBalance(query.CustId, query.CustLevel, query.Top);
}
