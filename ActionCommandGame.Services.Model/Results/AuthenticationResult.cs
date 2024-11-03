using ActionCommandGame.Services.Model.Core;

namespace ActionCommandGame.Services.Model.Results;

public class AuthenticationResult: ServiceResult
{
    public string? Token { get; set; }
    public string? UserId { get; set; }
}