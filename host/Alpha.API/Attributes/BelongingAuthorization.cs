using Alpha.Customers.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Alpha.API.Attributes;

[AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public sealed class BelongingAuthorization : Attribute, IAsyncAuthorizationFilter
{
    // private readonly ISubAccService subAccService;

    // public BelongingAuthorization(ISubAccService subAccService)
    //     => this.subAccService = subAccService;

    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        throw new NotImplementedException();
    }
}
