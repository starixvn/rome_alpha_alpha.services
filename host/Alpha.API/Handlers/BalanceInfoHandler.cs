using Alpha.API.Requests.Reports.BalanceInfo;
using Alpha.Reports.Models.BalanceInfo;
using Alpha.Reports.Services;
using MediatR;

namespace Alpha.API.Handlers;

public class BalanceInfoHandler :
    IRequestHandler<GetMyBalanceQuery, MyBalance>,
    IRequestHandler<GetTopBalanceUsernameQuery, IEnumerable<string>>,
    IRequestHandler<GetTopBalanceQuery, TopBalanceReport>
{
    private readonly IBalanceInfoService balanceInfoService;

    public BalanceInfoHandler(IBalanceInfoService balanceInfoService)
        => this.balanceInfoService = balanceInfoService;

    public Task<MyBalance> Handle(GetMyBalanceQuery query, CancellationToken cancellationToken)
        => balanceInfoService.GetMyBalanceAsync(query.CustId, query.CustLevel);

    public Task<IEnumerable<string>> Handle(GetTopBalanceUsernameQuery query, CancellationToken cancellationToken)
        => balanceInfoService.GetTopUsernameAsync(query.CustId, query.CustLevel, query.Top);

    public Task<TopBalanceReport> Handle(GetTopBalanceQuery query, CancellationToken cancellationToken)
        => balanceInfoService.GetTopBalanceReportAsync(query.CustId, query.CustLevel, query.Top);
}
