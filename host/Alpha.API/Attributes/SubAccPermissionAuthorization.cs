using Alpha.Customers.Services;
using Alpha.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Alpha.API.Attributes;

[AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public sealed class SubAccPermissionAuthorization : Attribute, IAsyncAuthorizationFilter
{
    private readonly ICustomerService customerService;

    public SubAccPermissionAuthorization(ICustomerService customerService)
        => this.customerService = customerService;

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var queryString = context.HttpContext.Request.Query;

        if (int.TryParse(queryString["subAccId"], out var subAccId)
            && subAccId > 0
            && int.TryParse(queryString["custId"], out var custId))
        {
            if (!(await customerService.IsSubAccHasReportPermissionAsync(custId, subAccId)))
            {
                throw new NotAuthorizedException($"CustId: {custId} - SubAccId: {subAccId} doesn't has Reports permission.");
            }
        }
    }
}
