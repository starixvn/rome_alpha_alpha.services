using Alpha.Reports.Models.BalanceInfo;
using MediatR;

namespace Alpha.API.Requests.Reports.BalanceInfo;

public record GetTopBalanceQuery(int CustId, byte CustLevelId, int Top) :
    ReportBaseQuery(CustId, CustLevelId),
    IRequest<IEnumerable<TopBalance>>;
