using System.Net.Http.Json;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Sdk;

public class GameSdk(IHttpClientFactory httpClientFactory)
{
    // PerformAction(playerId) - Perform action
    // buy(playerid, itemid) - Buy item
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    
    public async Task<ServiceResult<GameResult>> PerformAction(int playerId)
    {
        var client = _httpClientFactory.CreateClient("ActionCommandGame");
        var response = await client.GetAsync($"api/game/action/{playerId}");
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<ServiceResult<GameResult>>();
        if (result is null)
        {
            return new ServiceResult<GameResult>();
        }

        return result;
    }
    
    public async Task<ServiceResult<BuyResult>> Buy(int playerId, int itemId)
    {
        var client = _httpClientFactory.CreateClient("ActionCommandGame");
        var response = await client.GetAsync($"api/game/buy/{playerId}/{itemId}");
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<ServiceResult<BuyResult>>();
        if (result is null)
        {
            return new ServiceResult<BuyResult>();
        }

        return result;
    }
    
    public async Task<AppSettingsResult> GetAppSettings()
    {
        var client = _httpClientFactory.CreateClient("ActionCommandGame");
        var response = await client.GetAsync("api/game/settings");
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<AppSettingsResult>();
        if (result is null)
        {
            return new AppSettingsResult();
        }

        return result;
    }
}