using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Alpha.Shared.Infrastructure;

public sealed class HmacAuthenticationHandler : AuthenticationHandler<HmacAuthenticationSchemeOptions>
{
    private readonly Fanex.Caching.ICacheService cacheService;

    public HmacAuthenticationHandler(
        IOptionsMonitor<HmacAuthenticationSchemeOptions> options,
        ILoggerFactory loggerFactory,
        UrlEncoder encoder,
        ISystemClock clock,
        Fanex.Caching.ICacheService cacheService) : base(options, loggerFactory, encoder, clock)
    {
        this.cacheService = cacheService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        => Options.DisableHMACAuthentication || await ValidateAsync(Request)
        ? CreateSuccessAuthenticateResultAsync()
        : AuthenticateResult.Fail("Missing or malformed Authorization header.");

    private AuthenticateResult CreateSuccessAuthenticateResultAsync()
    {
        /*var identity = new ClaimsIdentity(Options.AuthenticationType);
        var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), Options.AuthenticationType);

        return AuthenticateResult.Success(ticket);*/

        var claimsToSet = new Claim[] { new Claim(ClaimTypes.NameIdentifier, Options.AppId) };

        var principal = new ClaimsPrincipal(new ClaimsIdentity(claimsToSet, Options.AuthenticationType, ClaimTypes.NameIdentifier, ClaimTypes.Role));
        var ticket = new AuthenticationTicket(principal, new AuthenticationProperties(), Options.AuthenticationScheme);

        return AuthenticateResult.Success(ticket);
    }

    private Task<bool> ValidateAsync(HttpRequest request)
    {
        if (!request.Headers.TryGetValue("Authorization", out var header))
        {
            return Task.FromResult(false);
        }

        var authenticationHeader = AuthenticationHeaderValue.Parse(header);

        if (!Options.AuthenticationScheme.Equals(authenticationHeader.Scheme))
        {
            return Task.FromResult(false);
        }

        var authenticationHeaderArray = GetAutherizationHeaderValues(authenticationHeader.Parameter);

        if (authenticationHeaderArray.Length == 0)
        {
            return Task.FromResult(false);
        }

        var appId = authenticationHeaderArray[0];

        if (!Options.AppId.Equals(appId))
        {
            return Task.FromResult(false);
        }

        var incomingBase64Signature = authenticationHeaderArray[1];
        var nonce = authenticationHeaderArray[2];
        var requestTimeStamp = authenticationHeaderArray[3];

        return IsValidRequestAsync(request, appId, incomingBase64Signature, nonce, requestTimeStamp);
    }

    private static string[] GetAutherizationHeaderValues(string? rawAuthenticationHeader)
    {
        if (rawAuthenticationHeader != null)
        {
            var credArray = rawAuthenticationHeader.Split(':');

            if (credArray.Length == 4)
            {
                return credArray;
            }
        }

        return Array.Empty<string>();
    }

    private async Task<bool> IsValidRequestAsync(
        HttpRequest request,
        string appId,
        string incomingBase64Signature,
        string nonce,
        string requestTimeStamp)
    {
        if (await IsReplayRequestAsync(nonce, requestTimeStamp))
        {
            return false;
        }

        var requestHttpMethod = request.Method;
        var absoluteUri = $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}{request.QueryString}";
        var requestUri = HttpUtility.UrlEncode(absoluteUri.ToLowerInvariant());

        var signatureRawData = $"{appId}{requestHttpMethod}{requestUri}{requestTimeStamp}{nonce}";
        var signature = Encoding.UTF8.GetBytes(signatureRawData);

        var secretKeyByteArray = Convert.FromBase64String(Options.APIKey);

        using (var hmac = new HMACSHA256(secretKeyByteArray))
        {
            var signatureBytes = hmac.ComputeHash(signature);
            return incomingBase64Signature.Equals(Convert.ToBase64String(signatureBytes), StringComparison.Ordinal);
        }
    }

    private async Task<bool> IsReplayRequestAsync(string nonce, string requestTimeStamp)
    {
        if (await cacheService.ContainAsync(nonce))
        {
            return true;
        }

        var epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
        var currentTs = DateTime.UtcNow - epochStart;

        var serverTotalSeconds = Convert.ToInt64(currentTs.TotalSeconds);
        var requestTotalSeconds = Convert.ToInt64(requestTimeStamp);

        if ((serverTotalSeconds - requestTotalSeconds) > Options.RequestMaxAgeInSeconds)
        {
            return true;
        }

        await cacheService.SetAsync(
            nonce,
            requestTimeStamp,
            new Fanex.Caching.CacheItemOptions
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(Options.RequestMaxAgeInSeconds)
            });

        return false;
    }

    private static byte[] ComputeHash(byte[] body)
    {
        using (var md5 = MD5.Create())
        {
            return md5.ComputeHash(body);
        }
    }
}
