using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alpha.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = "HMAC")]
public abstract class ControllerBaseApi
{
    private readonly IMediator mediator;

    protected ControllerBaseApi(IMediator mediator)
        => this.mediator = mediator;

    protected Task<TResponse> ObjectAsync<TResponse>(IRequest<TResponse> query)
        => mediator.Send(query);
}
