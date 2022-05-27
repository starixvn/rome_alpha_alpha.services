using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Alpha.API.Client.Exceptions;
using Alpha.API.Client.Models;

namespace Alpha.API.Client.Services;

public class ServiceBase
{
    protected readonly HttpClient httpClient;

    protected ServiceBase(HttpClient httpClient)
        => this.httpClient = httpClient;

    public async Task<T> Get<T>(string url)
    {
        var response = await httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return default;
            }

            return await response.Content.ReadFromJsonAsync<T>().ConfigureAwait(false);
        }

        var apiError = await response.Content.ReadFromJsonAsync<ApiError>().ConfigureAwait(false);
        throw BuildException(apiError);
    }

    private static Exception BuildException(ApiError apiError)
        => apiError.Code switch
        {
            ApiErrorCode.MinimizeUM => BuildMinimizeUMException(apiError),
            _ => new Exception(apiError.Message)
        };

    private static Exception BuildMinimizeUMException(ApiError apiError)
        => new MinimizeUMException(
            apiError.Message,
            apiError.Data["startTime"].ToString(),
            apiError.Data["endTime"].ToString());

    public async Task<T> Post<T>(string url, object data)
    {
        var response = await httpClient.PostAsync(
            url,
            new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json"));
        
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<T>().ConfigureAwait(false)
            : default;
    }
}
