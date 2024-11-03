using ActionCommandGame.Sdk;
using ActionCommandGame.Services.Model.Authentication;
using ActionCommandGame.Services.Model.Requests;
using ActionCommandGame.Ui.ConsoleApp.Abstractions;
using ActionCommandGame.Ui.ConsoleApp.ConsoleWriters;
using ActionCommandGame.Ui.ConsoleApp.Navigation;
using ActionCommandGame.Ui.ConsoleApp.Stores;

namespace ActionCommandGame.Ui.ConsoleApp.Views;

internal class SignUpView: IView
{
    private readonly IdentitySdk _identityService;
    private readonly NavigationManager _navigationManager;
    private readonly IBearerTokenStore _bearerTokenStore;
    private readonly MemoryStore _memoryStore;
    
    public SignUpView(
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
            ConsoleWriter.WriteText("Please Register, give Email or write back:", ConsoleColor.Yellow);

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "back":
                    await Login();
                    break;
                default:
                    await Register(choice);
                    break;
            }
        }
    }

    private async Task Login()
    {
        await _navigationManager.NavigateTo<SignInView>();
    }

    private async Task Register(string email)
    {
        ConsoleWriter.WriteText("Enter your password:", ConsoleColor.Yellow);
        var password = Console.ReadLine();
        
        var user = new UserRegisterRequest{Email = email, Password = password};
        var result = await _identityService.SignUp(user);

        if (result.IsSuccess)
        {
            _memoryStore.CurrentUserId = result.UserId;
            _bearerTokenStore.SetToken(result.Token);
            ConsoleWriter.WriteText("Registration successful! You can now log in.", ConsoleColor.Green);
            await _navigationManager.NavigateTo<PlayerSelectionView>();
        }
        else
        {
            ConsoleWriter.WriteText("Registration failed. Please try again.", ConsoleColor.Red);
        }
    }
}