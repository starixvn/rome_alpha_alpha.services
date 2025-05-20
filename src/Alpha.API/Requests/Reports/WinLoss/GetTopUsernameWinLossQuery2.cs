using MediatR;

namespace Alpha.API.Requests.Reports.WinLoss;

public record GetTopUsernameWinLossQuery2(
    int CustId,
    byte CustLevelId,
    DateTime FromDate,
    DateTime ToDate,
    string GroupProduct,
    int Top) : ReportBaseQuery(CustId, CustLevelId), IRequest<IEnumerable<string>>;