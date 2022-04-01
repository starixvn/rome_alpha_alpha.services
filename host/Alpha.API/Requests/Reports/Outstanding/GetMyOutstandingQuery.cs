using Alpha.Reports.Models.Outstanding;
using MediatR;

namespace Alpha.API.Requests.Reports.Outstanding;

public record GetMyOutstandingQuery(int CustId, int SubAccId, byte CustLevelId) :
    ReportBaseQuery(CustId, SubAccId, CustLevelId),
    IRequest<MyOutstanding>;
