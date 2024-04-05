using Alpha.API.Requests.TotalBets;
using Alpha.Reports.Models.TotalBets;
using Alpha.Reports.Services;
using MediatR;

namespace Alpha.API.Handlers;

public class TotalBetsHandler :
    IRequestHandler<TotalBetsQuery, TotalBetsModel>,
    IRequestHandler<TotalBetsDetailsQuery, TotalBetsDetailsModel>
{
    private readonly ITotalBetsService service;

    public TotalBetsHandler(ITotalBetsService service)
        => this.service = service;

    public Task<TotalBetsModel> Handle(TotalBetsQuery query, CancellationToken cancellationToken)
        => service.Summary(query.CustId, query.IsApplyPT, query.Language);

    public Task<TotalBetsDetailsModel> Handle(TotalBetsDetailsQuery query, CancellationToken cancellationToken)
        => service.Details(query.CustId, query.MatchId, query.IsApplyPT, query.Language);
}
