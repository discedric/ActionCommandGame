﻿using System.Net.Http.Json;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Filters;
using ActionCommandGame.Services.Model.Requests;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Sdk;

public class PlayerSdk(IHttpClientFactory httpClientFactory)
{
    // Find(filter) - Get all players
    // Get(id) - Get player by id
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    
    public async Task<ServiceResult<IList<PlayerResult>>> Find(PlayerFilter? filter)
    {
        var client = _httpClientFactory.CreateClient("ActionCommandGame");
        var route = filter is null ? "" : $"?FilterUserPlayers={filter.FilterUserPlayers}&UserId={filter.UserId}";
        var response = await client.GetAsync($"api/player{route}");
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<ServiceResult<IList<PlayerResult>>>();
        if (result is null)
        {
            return new ServiceResult<IList<PlayerResult>>();
        }

        return result;
    }
    
    public async Task<ServiceResult<PlayerResult>> Get(int id)
    {
        var client = _httpClientFactory.CreateClient("ActionCommandGame");
        var response = await client.GetAsync($"api/player/{id}");
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<ServiceResult<PlayerResult>>();
        if (result is null)
        {
            return new ServiceResult<PlayerResult>();
        }

        return result;
    }
    
    public async Task<ServiceResult<PlayerResult>> Create(PlayerCreate playerCreate)
    {
        var client = _httpClientFactory.CreateClient("ActionCommandGame");
        var response = await client.PostAsJsonAsync("api/player", playerCreate);
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<ServiceResult<PlayerResult>>();
        if (result is null)
        {
            return new ServiceResult<PlayerResult>();
        }

        return result;
    }
    // delete(id) - Delete player by id
    public async Task<ServiceResult<PlayerResult>> Delete(int id)
    {
        var client = _httpClientFactory.CreateClient("ActionCommandGame");
        var response = await client.DeleteAsync($"api/player/{id}");
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<ServiceResult<PlayerResult>>();
        if (result is null)
        {
            return new ServiceResult<PlayerResult>();
        }

        return result;
    }
}