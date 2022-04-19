using Alpha.Reports.Models.WinLoss;
using MediatR;

namespace Alpha.API.Requests.Reports.WinLoss;

public record GetDownlineWinLossQuery(int CustId, byte CustLevelId, DateTime Date, int DownlineId) :
    ReportBaseQuery(CustId, CustLevelId),
    IRequest<WinLossBase>;