using MediatR;

namespace Alpha.API.Requests.Reports.BalanceInfo;

public record GetTopBalanceUsernameQuery(int CustId, int SubAccId, byte CustLevelId, int Top) :
    ReportBaseQuery(CustId, SubAccId, CustLevelId),
    IRequest<IEnumerable<string>>;
