﻿using ActionCommandGame.Sdk.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace ActionCommandGame.Sdk.Extensions;

public static class ServiceCollectionExtensions
{
    public async static Task<IServiceCollection> AddApi(this IServiceCollection services, string apiUrl)
    {
        services.AddScoped<AuthorizationHandler>();
        
        services.AddHttpClient("ActionCommandGame", options =>
        {
            options.BaseAddress = new Uri(apiUrl);
        }).AddHttpMessageHandler<AuthorizationHandler>();
            
        services.AddScoped<IdentitySdk>();
        services.AddScoped<GameSdk>();
        services.AddScoped<ItemSdk>();
        services.AddScoped<PlayerSdk>();
        services.AddScoped<PlayerItemSdk>();
        
        var serviceProvider = services.BuildServiceProvider();
        var gameSdk = serviceProvider.GetRequiredService<GameSdk>();
        var appSettings = await gameSdk.GetAppSettings();
        services.AddSingleton(appSettings);

        return services;
    }
}