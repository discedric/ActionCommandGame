using System.Net.Http.Json;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Sdk;

public class ItemSdk(IHttpClientFactory httpClientFactory)
{
    // Find() - Get all items
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    
    public async Task<ServiceResult<IList<ItemResult>>> Find()
    {
        var client = _httpClientFactory.CreateClient("ActionCommandGame");
        var response = await client.GetAsync("api/item");
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<ServiceResult<IList<ItemResult>>>();
        if (result is null)
        {
            return new ServiceResult<IList<ItemResult>>();
        }

        return result;
    }
}