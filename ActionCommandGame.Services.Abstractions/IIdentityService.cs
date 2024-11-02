using ActionCommandGame.Services.Model.Requests;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Services.Abstractions;

public interface IIdentityService
{
    Task<AuthenticationResult> SignIn(UserSignInRequest request);
    Task<AuthenticationResult> SignUp(UserRegisterRequest request);
}