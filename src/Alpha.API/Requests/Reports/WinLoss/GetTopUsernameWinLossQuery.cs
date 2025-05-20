using MediatR;

namespace Alpha.API.Requests.Reports.WinLoss;

public record GetTopUsernameWinLossQuery(int CustId, byte CustLevelId, DateTime Date, int Top) :
    ReportBaseQuery(CustId, CustLevelId),
    IRequest<IEnumerable<string>>;