using System.Net.Http.Json;
using Alpha.API.Client.Models;

namespace Alpha.API.Client.Services;

public class ServiceBase
{
    protected readonly HttpClient httpClient;

    protected ServiceBase(HttpClient httpClient)
        => this.httpClient = httpClient;

    public Task<T> GetAsync<T>(string url)
        => httpClient.GetFromJsonAsync<T>(url);

    public async Task<APIResult<T>> GetAsync_NEW<T>(string url)
    {
        var response = await httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            return new APIResult<T>(await response.Content.ReadFromJsonAsync<T>());
        }
        
        return new APIResult<T>(default(T), 401, await response.Content.ReadAsStringAsync());
    }
}
