using Alpha.Reports.Models.WinLoss;
using MediatR;

namespace Alpha.API.Requests.Reports.WinLoss;

public record GetWinLossQuery2(
    int CustId,
    byte CustLevelId,
    DateTime FromDate,
    DateTime ToDate,
    string GroupProduct) : ReportBaseQuery(CustId, CustLevelId), IRequest<WinLossBase>;