using ActionCommandGame.Sdk;
using ActionCommandGame.Services.Model.Requests;
using ActionCommandGame.Ui.ConsoleApp.Abstractions;
using ActionCommandGame.Ui.ConsoleApp.ConsoleWriters;
using ActionCommandGame.Ui.ConsoleApp.Navigation;
using ActionCommandGame.Ui.ConsoleApp.Stores;

namespace ActionCommandGame.Ui.ConsoleApp.Views;

internal class CreatePlayerView: IView
{
    private readonly MemoryStore _memoryStore;
    private readonly PlayerSdk _playerService;
    private readonly NavigationManager _navigationManager;
    
    public CreatePlayerView(
        MemoryStore memoryStore,
        PlayerSdk playerService,
        NavigationManager navigationManager)
    {
        _memoryStore = memoryStore;
        _playerService = playerService;
        _navigationManager = navigationManager;
    }
    
    public async Task Show()
    {
        ConsoleBlockWriter.Write("Type 'logout' to leave.", 1, ConsoleColor.White);
        ConsoleBlockWriter.Write("Type 'back' to go back.",1, ConsoleColor.White);
        ConsoleBlockWriter.Write("Create a new Player", 1, ConsoleColor.White);
        ConsoleWriter.WriteText("Player name: ", ConsoleColor.White, false);
        var playerName = Console.ReadLine();
        
        if(playerName.ToLower() == "logout")
        {
            _memoryStore.CurrentUserId = null;
            await _navigationManager.NavigateTo<SignInView>(false);
            return;
        }
        
        if(playerName.ToLower() == "back")
        {
            await _navigationManager.NavigateTo<PlayerSelectionView>();
            return;
        }
        
        if (string.IsNullOrWhiteSpace(playerName))
        {
            ConsoleBlockWriter.Write("Player name cannot be empty.", 1, ConsoleColor.Red);
            await Show();
            return;
        }

        var playerCreate = new PlayerCreate
        {
            UserId = _memoryStore.CurrentUserId,
            Name = playerName
        };

        var result = await _playerService.Create(playerCreate);

        if (!result.IsSuccess)
        {
            ConsoleBlockWriter.Write("Could not create player.", 1, ConsoleColor.Red);
            await Show();
            return;
        }

        _memoryStore.CurrentPlayerId = result.Data.Id;

        await _navigationManager.NavigateTo<GameView>();
    }
}