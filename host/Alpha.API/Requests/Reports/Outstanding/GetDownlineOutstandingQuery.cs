using Alpha.Reports.Models.Outstanding;
using MediatR;

namespace Alpha.API.Requests.Reports.Outstanding;

public record GetDownlineOutstandingQuery(
    int CustId,
    int SubAccId,
    byte CustLevelId,
    int DownlineId,
    string Language) : ReportBaseQuery(CustId, SubAccId, CustLevelId), IRequest<IEnumerable<DownlineOutstanding>>;
