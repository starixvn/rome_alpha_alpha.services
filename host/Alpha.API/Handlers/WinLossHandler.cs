using Alpha.API.Requests.Reports.WinLoss;
using Alpha.Reports.Models.WinLoss;
using Alpha.Reports.Services;
using MediatR;

namespace Alpha.API.Handlers;

public class WinLossHandler : IRequestHandler<GetWinLossQuery, WinLossBase>
{
    private readonly IWinLossService winLossService;

    public WinLossHandler(IWinLossService winLossService)
        => this.winLossService = winLossService;
    
    public Task<WinLossBase> Handle(GetWinLossQuery query, CancellationToken cancellationToken)
        => winLossService.GetWinLossAsync(query.CustId, query.CustLevel, query.Date);
}
