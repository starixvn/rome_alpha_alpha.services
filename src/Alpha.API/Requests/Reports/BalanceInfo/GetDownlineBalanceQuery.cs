using Alpha.Reports.Models.BalanceInfo;
using MediatR;

namespace Alpha.API.Requests.Reports.BalanceInfo;

public record GetDownlineBalanceQuery(int CustId, byte CustLevelId, int DownlineId) :
    ReportBaseQuery(CustId, CustLevelId),
    IRequest<DownlineBalance>;
