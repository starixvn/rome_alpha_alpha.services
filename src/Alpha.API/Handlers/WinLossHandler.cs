using Alpha.API.Requests.Reports.WinLoss;
using Alpha.Reports.Models.WinLoss;
using Alpha.Reports.Services;
using MediatR;

namespace Alpha.API.Handlers;

public class WinLossHandler :
    IRequestHandler<GetWinLossQuery, WinLossBase>,
    IRequestHandler<GetDownlineWinLossQuery, WinLossBase>,
    IRequestHandler<GetTopUsernameWinLossQuery, IEnumerable<string>>
{
    private readonly IWinLossService winLossService;

    public WinLossHandler(IWinLossService winLossService)
        => this.winLossService = winLossService;
    
    public Task<WinLossBase> Handle(GetWinLossQuery query, CancellationToken cancellationToken)
        => winLossService.GetWinLoss(query.CustId, query.CustLevel, query.Date);

    public Task<WinLossBase> Handle(GetDownlineWinLossQuery query, CancellationToken cancellationToken)
        => winLossService.GetDownlineWinLoss(query.CustId, query.CustLevel, query.Date, query.DownlineId);

    public Task<IEnumerable<string>> Handle(GetTopUsernameWinLossQuery query, CancellationToken cancellationToken)
        => winLossService.GetTopUsername(query.CustId, query.CustLevel, query.Date, query.Top);
}
