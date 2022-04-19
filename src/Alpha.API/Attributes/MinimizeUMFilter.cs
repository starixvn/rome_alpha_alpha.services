using Alpha.Reports.Services;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Alpha.API.Attributes;

[AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public sealed class MinimizeUMFilter : Attribute, IAuthorizationFilter
{
    private readonly IMinimizeUMService minimizeUMService;

    public MinimizeUMFilter(IMinimizeUMService minimizeUMService)
        => this.minimizeUMService = minimizeUMService;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context?.ActionDescriptor is ControllerActionDescriptor descriptor)
        {
            minimizeUMService.ThrowIfUM(descriptor.ControllerName);
        }
    }
}
