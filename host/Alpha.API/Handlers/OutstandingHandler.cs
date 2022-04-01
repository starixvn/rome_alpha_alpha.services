using Alpha.API.Requests.Reports.Outstanding;
using Alpha.Reports.Models.Outstanding;
using Alpha.Reports.Params.Outstanding;
using Alpha.Reports.Services;
using MediatR;

namespace Alpha.API.Handlers;

public class OutstandingHandler :
    IRequestHandler<GetMyOutstandingQuery, MyOutstanding>,
    IRequestHandler<GetDownlineOutstandingQuery, IEnumerable<DownlineOutstanding>>,
    IRequestHandler<GetTopOutstandingUsernameQuery, IEnumerable<string>>,
    IRequestHandler<GetTopOutstandingQuery, TopOutstanding>
{
    private readonly IOutstandingService outstandingService;

    public OutstandingHandler(IOutstandingService outstandingService)
        => this.outstandingService = outstandingService;

    public Task<MyOutstanding> Handle(GetMyOutstandingQuery query, CancellationToken cancellationToken)
        => outstandingService.GetMyOutstandingAsync(query.CustId, query.CustLevel);

    public Task<IEnumerable<DownlineOutstanding>> Handle(GetDownlineOutstandingQuery query, CancellationToken cancellationToken)
        => outstandingService.GetDownlineOutstandingAsync(
            new GetDownlineOutstandingParam(query.CustId, query.CustLevel, query.DownlineId, query.Language));
    
    public Task<IEnumerable<string>> Handle(GetTopOutstandingUsernameQuery query, CancellationToken cancellationToken)
        => outstandingService.GetTopUsernameAsync(query.CustId, query.CustLevel, query.Top);
    
    public Task<TopOutstanding> Handle(GetTopOutstandingQuery query, CancellationToken cancellationToken)
        => outstandingService.GetTopOutstandingAsync(query.CustId, query.CustLevel, query.Top);
}
