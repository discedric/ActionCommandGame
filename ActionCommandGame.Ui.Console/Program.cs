using System.Collections.Immutable;
using System.Text;
using ActionCommandGame.Sdk;
using ActionCommandGame.Sdk.Extensions;
using ActionCommandGame.Services.Model.Authentication;
using ActionCommandGame.Ui.ConsoleApp.Navigation;
using ActionCommandGame.Ui.ConsoleApp.Settings;
using ActionCommandGame.Ui.ConsoleApp.Stores;
using ActionCommandGame.Ui.ConsoleApp.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ActionCommandGame.Ui.ConsoleApp
{
    class Program
    {
        private static IServiceProvider? ServiceProvider { get; set; }
        private static IConfiguration? Configuration { get; set; }

        static async Task Main()
        {
            var directory = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            
            Configuration = builder.Build();
            
            var serviceCollection = new ServiceCollection();
            await ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            
            var navigationManager = ServiceProvider.GetRequiredService<NavigationManager>();
            
            Console.OutputEncoding = Encoding.UTF8;
            
            await navigationManager.NavigateTo<TitleView>();
        }

        
        public static async Task ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<MemoryStore>();
            
            var apiSettings = new ApiSettings();
            Configuration?.GetSection(nameof(ApiSettings)).Bind(apiSettings);
            services.AddScoped<IBearerTokenStore,BearerTokenStore>();
            
            await services.AddApi(apiSettings.BaseUrl);
            
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/SignIn";
                    options.AccessDeniedPath = "/SignIn";
                });

            // Register Navigation
            services.AddTransient<NavigationManager>();

            // Register the Views
            services.AddTransient<ExitView>();
            services.AddTransient<GameView>();
            services.AddTransient<HelpView>();
            services.AddTransient<InventoryView>();
            services.AddTransient<LeaderboardView>();
            services.AddTransient<PlayerSelectionView>();
            services.AddTransient<CreatePlayerView>();
            services.AddTransient<ShopView>();
            services.AddTransient<TitleView>();
            services.AddTransient<SignInView>();
            services.AddTransient<SignUpView>();
        }
    }
}
