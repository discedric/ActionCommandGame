using System.Text;
using ActionCommandGame.Configuration;
using ActionCommandGame.Repository;
using ActionCommandGame.Services;
using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Ui.ConsoleApp.Navigation;
using ActionCommandGame.Ui.ConsoleApp.Stores;
using ActionCommandGame.Ui.ConsoleApp.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ActionCommandGame.Ui.ConsoleApp
{
    class Program
    {
        private static IServiceProvider? ServiceProvider { get; set; }
        private static IConfiguration? Configuration { get; set; }

        static async Task Main()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            
            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            var navigationManager = ServiceProvider.GetRequiredService<NavigationManager>();

            var dbContext = ServiceProvider.GetRequiredService<ActionCommandGameDbContext>();
            dbContext.Initialize();

            Console.OutputEncoding = Encoding.UTF8;
            
            await navigationManager.NavigateTo<TitleView>();
        }

        public static void ConfigureServices(IServiceCollection services)

        {
            var appSettings = new AppSettings();
            Configuration?.Bind(nameof(AppSettings), appSettings);
            services.AddSingleton(appSettings);
            
            services.AddDbContext<ActionCommandGameDbContext>(options =>
                options.UseInMemoryDatabase(nameof(ActionCommandGameDbContext)));

            services.AddSingleton<MemoryStore>();

            //Register Navigation
            services.AddTransient<NavigationManager>();

            //Register the Views
            services.AddTransient<ExitView>();
            services.AddTransient<GameView>();
            services.AddTransient<HelpView>();
            services.AddTransient<InventoryView>();
            services.AddTransient<LeaderboardView>();
            services.AddTransient<PlayerSelectionView>();
            services.AddTransient<ShopView>();
            services.AddTransient<TitleView>();

            //Register Services
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<INegativeGameEventService, NegativeGameEventService>();
            services.AddScoped<IPositiveGameEventService, PositiveGameEventService>();
            services.AddScoped<IPlayerItemService, PlayerItemService>();
            services.AddScoped<IPlayerService, PlayerService>();
        }
    }
}
