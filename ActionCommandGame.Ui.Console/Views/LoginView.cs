using ActionCommandGame.Sdk;
using ActionCommandGame.Services.Model.Requests;
using ActionCommandGame.Ui.ConsoleApp.Abstractions;
using ActionCommandGame.Ui.ConsoleApp.ConsoleWriters;
using ActionCommandGame.Ui.ConsoleApp.Navigation;

namespace ActionCommandGame.Ui.ConsoleApp.Views;

internal class LoginView: IView
{
    private readonly IdentitySdk _identityService;
    private readonly NavigationManager _navigationManager;
    
    public LoginView(IdentitySdk identityService, NavigationManager navigationManager)
    {
        _identityService = identityService;
        _navigationManager = navigationManager;
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
                    await Register();
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
            ConsoleWriter.WriteText("Login successful!", ConsoleColor.Green);
            await _navigationManager.NavigateTo<PlayerSelectionView>();
        }
        else
        {
            ConsoleWriter.WriteText("Login failed. Please try again.", ConsoleColor.Red);
        }
    }

    private async Task Register()
    {
        ConsoleWriter.WriteText("Enter your email:", ConsoleColor.Yellow);
        var email = Console.ReadLine();

        ConsoleWriter.WriteText("Enter your password:", ConsoleColor.Yellow);
        var password = Console.ReadLine();
        
        var user = new UserRegisterRequest{Email = email, Password = password};
        var result = await _identityService.SignUp(user);

        if (result.IsSuccess)
        {
            ConsoleWriter.WriteText("Registration successful! You can now log in.", ConsoleColor.Green);
            await _navigationManager.NavigateTo<PlayerSelectionView>();
        }
        else
        {
            ConsoleWriter.WriteText("Registration failed. Please try again.", ConsoleColor.Red);
        }
    }
}