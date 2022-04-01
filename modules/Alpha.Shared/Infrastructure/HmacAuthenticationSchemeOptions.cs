using Microsoft.AspNetCore.Authentication;

namespace Alpha.Shared.Infrastructure;

public sealed class HmacAuthenticationSchemeOptions : AuthenticationSchemeOptions
{
    public bool DisableHMACAuthentication { get; set; }

    public string AppId { get; set; } = default!;

    public string APIKey { get; set; } = default!;

    public string AuthenticationType { get; set; } = "HMAC";

    public string AuthenticationScheme { get; set; } = "amx";

    public long RequestMaxAgeInSeconds { get; set; } = 300; // Means 5 min

}
