using Alpha.API.Requests.Reports.WinLoss;
using Alpha.Reports.Consts;
using Alpha.Reports.Models.WinLoss;
using Alpha.Reports.Services;
using MediatR;

namespace Alpha.API.Handlers;

public class WinLossHandler :
    IRequestHandler<GetWinLossQuery, WinLossBase>,
    IRequestHandler<GetDownlineWinLossQuery, WinLossBase>,
    IRequestHandler<GetTopUsernameWinLossQuery, IEnumerable<string>>,
    IRequestHandler<GetEnteredResultQuery, EnteredResult>,
    IRequestHandler<GetTopUsernameWinLossQuery2, IEnumerable<string>>,
    IRequestHandler<GetWinLossQuery2, WinLossBase>
{
    private readonly IWinLossService winLossService;

    public WinLossHandler(IWinLossService winLossService)
        => this.winLossService = winLossService;

    public Task<WinLossBase> Handle(GetWinLossQuery query, CancellationToken cancellationToken)
        => winLossService.GetWinLoss(query.CustId, query.CustLevel, query.Date, query.Date, GroupProductConst.All);

    public Task<WinLossBase> Handle(GetDownlineWinLossQuery query, CancellationToken cancellationToken)
        => winLossService.GetDownlineWinLoss(query.CustId, query.CustLevel, query.Date, query.Date, GroupProductConst.All, query.DownlineId);

    public Task<IEnumerable<string>> Handle(GetTopUsernameWinLossQuery query, CancellationToken cancellationToken)
        => winLossService.GetTopUsername(query.CustId, query.CustLevel, query.Date, query.Date, GroupProductConst.All, query.Top);

    public Task<EnteredResult> Handle(GetEnteredResultQuery query, CancellationToken cancellationToken)
        => winLossService.GetYesterdayEnteredSportResult();

    public Task<IEnumerable<string>> Handle(GetTopUsernameWinLossQuery2 request, CancellationToken cancellationToken)
        => winLossService.GetTopUsername(request.CustId, request.CustLevel, request.FromDate, request.ToDate, string.IsNullOrWhiteSpace(request.GroupProduct) ? GroupProductConst.All : request.GroupProduct, request.Top);

    public Task<WinLossBase> Handle(GetWinLossQuery2 query, CancellationToken cancellationToken)
        => winLossService.GetWinLoss(query.CustId, query.CustLevel, query.FromDate, query.ToDate, string.IsNullOrWhiteSpace(query.GroupProduct) ? GroupProductConst.All : query.GroupProduct);
}
