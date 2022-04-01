using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Alpha.API.Client.Infrastructure;

public class HmacDelegatingHandler : DelegatingHandler
{
    private readonly string appId;
    private readonly string apiKey;

    protected HmacDelegatingHandler(string appId, string apiKey)
    {
        this.appId = appId;
        this.apiKey = apiKey;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var requestHttpMethod = request.Method.Method;
        var requestUri = HttpUtility.UrlEncode(request.RequestUri?.AbsoluteUri.ToLowerInvariant());

        var epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
        var timeSpan = DateTime.UtcNow - epochStart;
        var requestTimeStamp = Convert.ToUInt64(timeSpan.TotalSeconds).ToString();

        var nonce = Guid.NewGuid().ToString("N");

        request.Headers.Authorization = BuildAuthenticationHeaderValue(
            requestHttpMethod,
            requestUri,
            requestTimeStamp,
            nonce);

        return base.SendAsync(request, cancellationToken);
    }

    private AuthenticationHeaderValue BuildAuthenticationHeaderValue(
        string requestHttpMethod,
        string requestUri,
        string requestTimeStamp,
        string nonce)
    {
        var signatureRawData = $"{appId}{requestHttpMethod}{requestUri}{requestTimeStamp}{nonce}";
        var signature = Encoding.UTF8.GetBytes(signatureRawData);

        var secretKeyByteArray = Convert.FromBase64String(apiKey);

        using (var hmac = new HMACSHA256(secretKeyByteArray))
        {
            var signatureBytes = hmac.ComputeHash(signature);
            var requestSignatureBase64String = Convert.ToBase64String(signatureBytes);

            return new AuthenticationHeaderValue(
                "amx",
                $"{appId}:{requestSignatureBase64String}:{nonce}:{requestTimeStamp}");
        }
    }
}
