using Microsoft.Extensions.Configuration;

namespace Alpha.API.Client.Infrastructure;

public sealed class AlphaAPIHmacDelegatingHandler : HmacDelegatingHandler
{
    public AlphaAPIHmacDelegatingHandler(IConfiguration configuration) : base(
        configuration.GetValue<string>("AlphaAPI:AppId"),
        configuration.GetValue<string>("AlphaAPI:APIKey"))
    {
    }
}
