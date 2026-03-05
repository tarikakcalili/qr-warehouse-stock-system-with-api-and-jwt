using System.Net.Http.Json;

namespace MegaQr.Web.Services;
public class ApiClientService
{
    private readonly IHttpClientFactory _factory;

    public ApiClientService(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    private HttpClient CreateClient()
    {
        return _factory.CreateClient("ApiClient");
    }

    public async Task<T> GetAsync<T>(string url)
    {
        var client = CreateClient();
        return await client.GetFromJsonAsync<T>(url);
    }

    public async Task<HttpResponseMessage> PostAsync<T>(string url, T data)
    {
        var client = CreateClient();
        return await client.PostAsJsonAsync(url, data);
    }
}
