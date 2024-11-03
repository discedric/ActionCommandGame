using ActionCommandGame.Services.Model.Authentication;
using Microsoft.AspNetCore.Http;

namespace ActionCommandGame.Ui.ConsoleApp.Stores;

public class BearerTokenStore : IBearerTokenStore
{
    private const string TokenFilePath = "token.txt"; // Path to store the token

    public string GetToken()
    {
        // Read the token from the file, if it exists
        if (System.IO.File.Exists(TokenFilePath))
        {
            return System.IO.File.ReadAllText(TokenFilePath);
        }

        return string.Empty;
    }

    public void SetToken(string token)
    {
        // Write the token to the file
        System.IO.File.WriteAllText(TokenFilePath, token);
    }
}