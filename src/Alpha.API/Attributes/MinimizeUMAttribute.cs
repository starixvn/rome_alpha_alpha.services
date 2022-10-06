using System.Diagnostics.CodeAnalysis;
using Alpha.Reports.Services;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Alpha.API.Attributes;

[ExcludeFromCodeCoverage]
[AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public sealed class MinimizeUMAttribute : Attribute, IAsyncAuthorizationFilter
{
    private readonly IMinimizeUMService minimizeUMService;

    public MinimizeUMAttribute(IMinimizeUMService minimizeUMService)
        => this.minimizeUMService = minimizeUMService;

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (context?.ActionDescriptor is ControllerActionDescriptor descriptor)
        {
            var custIdValue = context.HttpContext.Request.Query["CustId"];

            if (int.TryParse(custIdValue, out var custId))
            {
                await minimizeUMService.ThrowIfUM(descriptor.ControllerName, custId);
            }
        }
    }
}
