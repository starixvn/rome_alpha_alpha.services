using MediatR;

namespace Alpha.API.Requests.Reports.BalanceInfo;

public record GetTopBalanceUsernameQuery(int CustId, byte CustLevelId, int Top) :
    ReportBaseQuery(CustId, CustLevelId),
    IRequest<IEnumerable<string>>;
