using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ActionCommandGame.Configuration;
using ActionCommandGame.Model;
using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Services.Extensions;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Requests;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Services;

public class IdentityService(UserManager<ApplicationUser> userManager, JwtSettings jwtSettings) : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly JwtSettings _jwtSettings = jwtSettings;

    public async Task<AuthenticationResult> SignIn(UserSignInRequest request)
    {
        //Check user
        var existingUser = await _userManager.FindByEmailAsync(request.Email.ToLower());
        if (existingUser is null)
        {
            var result = new AuthenticationResult();
            result.LoginFailed();
            return result;
        }

        //Check password
        var hasValidPassword = await _userManager.CheckPasswordAsync(existingUser, request.Password);
        if (!hasValidPassword)
        {
            var result = new AuthenticationResult();
            result.LoginFailed();
            return result;
        }

        var token = GenerateJwtToken(existingUser);

        return new AuthenticationResult() { Token = token , UserId = existingUser.Id };
    }

    public async Task<AuthenticationResult> SignUp(UserRegisterRequest request)
    {
        // Check if the user already exists
        var existingUser = await _userManager.FindByEmailAsync(request.Email.ToLower());
        if (existingUser is not null)
        {
            return new AuthenticationResult
            {
                Messages = new List<ServiceMessage>
                {
                    new ServiceMessage
                    {
                        Code = "RegisterFailed",
                        Message = "User already exists."
                    }
                }
            };
        }

        var registerUser = new ApplicationUser
        {
            Email = request.Email.ToLower(),
            NormalizedEmail = request.Email.ToUpper(),
            UserName = request.Email.ToLower(),
            NormalizedUserName = request.Email.ToUpper()
        };

        var createUserResult = await _userManager.CreateAsync(registerUser, request.Password);
        if (!createUserResult.Succeeded)
        {
            return new AuthenticationResult
            {
                Messages = createUserResult.Errors.Select(e => new ServiceMessage
                {
                    Code = e.Code,
                    Message = e.Description,
                    MessagePriority = MessagePriority.Error
                }).ToList()
            };
        }
        // get new created user
        registerUser = await _userManager.FindByEmailAsync(request.Email.ToLower());
        // Create and return token
        var token = GenerateJwtToken(registerUser);
        return new AuthenticationResult { Token = token , UserId = registerUser.Id};
    }

    private string GenerateJwtToken(IdentityUser user)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("id", user.Id)
        };
        if (!string.IsNullOrWhiteSpace(user.UserName))
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName));
        }
        
        if(!string.IsNullOrWhiteSpace(user.Email))
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        }

        var expirationTime = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime);
        var notBeforeTime = DateTime.UtcNow; // Set the not-before time to now

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            NotBefore = notBeforeTime, // Set the NotBefore time
            Expires = expirationTime, // Set the expiration time to future
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var securityToken = handler.CreateToken(tokenDescriptor);
        
        return handler.WriteToken(securityToken);
    }
}