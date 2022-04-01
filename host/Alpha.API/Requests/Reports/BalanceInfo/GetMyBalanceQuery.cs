using Alpha.Reports.Models.BalanceInfo;
using MediatR;

namespace Alpha.API.Requests.Reports.BalanceInfo;

public record GetMyBalanceQuery(int CustId, int SubAccId, byte CustLevelId) :
    ReportBaseQuery(CustId, SubAccId, CustLevelId),
    IRequest<MyBalance>;
