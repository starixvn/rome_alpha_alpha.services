using Alpha.Reports.Models.WinLoss;
using MediatR;

namespace Alpha.API.Requests.Reports.WinLoss;

public record GetWinLossQuery(int CustId, int SubAccId, byte CustLevelId, DateTime Date) :
    ReportBaseQuery(CustId, SubAccId, CustLevelId),
    IRequest<WinLossBase>;