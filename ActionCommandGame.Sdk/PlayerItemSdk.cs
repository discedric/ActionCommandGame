using System.Net.Http.Json;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Filters;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Sdk;

public class PlayerItemSdk(IHttpClientFactory httpClientFactory)
{
    // Find(filter) - Get all player items
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    
    public async Task<ServiceResult<IList<PlayerItemResult>>> Find(PlayerItemFilter? filter)
    {
        var client = _httpClientFactory.CreateClient("ActionCommandGame");
        var route = filter is null ? "" : $"?PlayerId={filter.PlayerId}";
        var response = await client.GetAsync($"api/playeritem{route}");
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<ServiceResult<IList<PlayerItemResult>>>();
        if (result is null)
        {
            return new ServiceResult<IList<PlayerItemResult>>();
        }

        return result;
    }
}