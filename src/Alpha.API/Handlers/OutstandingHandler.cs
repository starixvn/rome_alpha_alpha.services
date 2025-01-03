using Alpha.API.Requests.Reports.Outstanding;
using Alpha.Reports.Models.Outstanding;
using Alpha.Reports.Params;
using Alpha.Reports.Services;
using MediatR;

namespace Alpha.API.Handlers;

public class OutstandingHandler :
    IRequestHandler<GetDownlineOutstandingQuery, OutstandingReport>,
    IRequestHandler<GetTopOutstandingUsernameQuery, IEnumerable<string>>,
    IRequestHandler<GetTopOutstandingQuery, OutstandingReport>
{
    private readonly IOutstandingService outstandingService;

    public OutstandingHandler(IOutstandingService outstandingService)
        => this.outstandingService = outstandingService;

    public Task<OutstandingReport> Handle(GetDownlineOutstandingQuery query, CancellationToken cancellationToken)
        => outstandingService.GetDownlineOutstanding(
            new GetDownlineOutstandingParam(query.CustId, query.CustLevel, query.DownlineId, query.Language, query.SiteLabel, query.DownlineLevel));
    
    public Task<IEnumerable<string>> Handle(GetTopOutstandingUsernameQuery query, CancellationToken cancellationToken)
        => outstandingService.GetTopUsername(query.CustId, query.CustLevel, query.Top);
    
    public Task<OutstandingReport> Handle(GetTopOutstandingQuery query, CancellationToken cancellationToken)
        => outstandingService.GetTopOutstanding(query.CustId, query.CustLevel, query.Top);
}
