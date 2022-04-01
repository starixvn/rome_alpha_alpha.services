using Alpha.Reports.Models.BalanceInfo;
using MediatR;

namespace Alpha.API.Requests.Reports.BalanceInfo;

public record GetTopBalanceQuery(int CustId, int SubAccId, byte CustLevelId, int Top) :
    ReportBaseQuery(CustId, SubAccId, CustLevelId),
    IRequest<TopBalanceReport>;
