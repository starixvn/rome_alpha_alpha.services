using Alpha.Reports.Models.WinLoss;
using MediatR;

namespace Alpha.API.Requests.Reports.WinLoss;

public record GetWinLossQuery(int CustId, byte CustLevelId, DateTime Date) :
    ReportBaseQuery(CustId, CustLevelId),
    IRequest<WinLossBase>;