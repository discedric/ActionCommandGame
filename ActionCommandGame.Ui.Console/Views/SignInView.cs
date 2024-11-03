using ActionCommandGame.Sdk;
using ActionCommandGame.Services.Model.Authentication;
using ActionCommandGame.Services.Model.Requests;
using ActionCommandGame.Ui.ConsoleApp.Abstractions;
using ActionCommandGame.Ui.ConsoleApp.ConsoleWriters;
using ActionCommandGame.Ui.ConsoleApp.Navigation;
using ActionCommandGame.Ui.ConsoleApp.Stores;

namespace ActionCommandGame.Ui.ConsoleApp.Views;

internal class SignInView: IView
{
    private readonly IdentitySdk _identityService;
    private readonly NavigationManager _navigationManager;
    private readonly IBearerTokenStore _bearerTokenStore;
    private readonly MemoryStore _memoryStore;
    
    public SignInView(
        IdentitySdk identityService, 
        NavigationManager navigationManager, 
        IBearerTokenStore bearerTokenStore,
        MemoryStore memoryStore)
    {
        _identityService = identityService;
        _navigationManager = navigationManager;
        _bearerTokenStore = bearerTokenStore;
        _memoryStore = memoryStore;
    }

    public async Task Show()
    {
        while (true)
        {
            ConsoleWriter.WriteText("Welcome! Please choose an option:", ConsoleColor.Yellow);
            ConsoleWriter.WriteText("1. Login");
            ConsoleWriter.WriteText("2. Register");
            ConsoleWriter.WriteText("3. Exit");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await Login();
                    break;
                case "2":
                    await _navigationManager.NavigateTo<SignUpView>();
                    break;
                case "3":
                    return;
                default:
                    ConsoleWriter.WriteText("Invalid choice. Please try again.", ConsoleColor.Red);
                    break;
            }
        }
    }
    private async Task Login()
    {
        ConsoleWriter.WriteText("Enter your email:", ConsoleColor.Yellow);
        var email = Console.ReadLine();

        ConsoleWriter.WriteText("Enter your password:", ConsoleColor.Yellow);
        var password = Console.ReadLine();
        
        var User = new UserSignInRequest{Email = email, Password = password};
        var result = await _identityService.SignIn(User);

        if (result.IsSuccess)
        {
            _memoryStore.CurrentUserId = result.UserId;
            _bearerTokenStore.SetToken(result.Token);
            ConsoleWriter.WriteText("Login successful!", ConsoleColor.Green);
            await _navigationManager.NavigateTo<PlayerSelectionView>();
        }
        else
        {
            ConsoleWriter.WriteText("Login failed. Please try again.", ConsoleColor.Red);
        }
    }
}