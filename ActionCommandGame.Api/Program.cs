using System.Text;
using ActionCommandGame.Configuration;
using ActionCommandGame.Model;
using ActionCommandGame.Repository;
using ActionCommandGame.Services;
using ActionCommandGame.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        var securitydefinition = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "JWT Authorization header using the Bearer scheme.",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey
        };
        options.AddSecurityDefinition("Bearer", securitydefinition);

        var securityScheme = new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        };
        
        var securityRequirement = new OpenApiSecurityRequirement
        {
            {
                securityScheme, new string[] { }
            }
        };
        options.AddSecurityRequirement(securityRequirement);
    });

var appSettings = new AppSettings();
builder.Configuration?.Bind(nameof(AppSettings), appSettings);
builder.Services.AddSingleton(appSettings);

builder.Services.AddDbContext<ActionCommandGameDbContext>()
.AddIdentity<IdentityUser, IdentityRole>()
.AddEntityFrameworkStores<ActionCommandGameDbContext>();

var jwtSettings = new JwtSettings();
builder.Configuration?.GetSection(nameof(JwtSettings)).Bind(jwtSettings);
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = false,
        ValidateLifetime = true
    };
});
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<INegativeGameEventService, NegativeGameEventService>();
builder.Services.AddScoped<IPositiveGameEventService, PositiveGameEventService>();
builder.Services.AddScoped<IPlayerItemService, PlayerItemService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IGameService, GameService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
