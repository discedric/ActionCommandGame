using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ActionCommandGame.Configuration;
using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Services.Extensions;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Requests;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Services;

public class IdentityService(UserManager<IdentityUser> userManager, JwtSettings jwtSettings) : IIdentityService
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
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

        return new AuthenticationResult() { Token = token };
    }
    
    public async Task<AuthenticationResult> SignUp(UserRegisterRequest request)
    {
        //Check user
        var existingUser = await _userManager.FindByEmailAsync(request.Email.ToLower());
        if (existingUser is not null)
        {
            var result = new AuthenticationResult();
            result.Messages.Add(new ServiceMessage
            {
                Code = "RegisterFailed", 
                Message = "User already exists."
            });
            return result;
        }

        var registerUser = new IdentityUser(request.Email.ToLower());
        var createUserResult = await _userManager.CreateAsync(registerUser, request.Password);
        if (!createUserResult.Succeeded)
        {
            var result = new AuthenticationResult();

            result.Messages = createUserResult.Errors.Select(e => new ServiceMessage
            {
                Code = e.Code,
                Message = e.Description,
                MessagePriority = MessagePriority.Error
            }).ToList();

            return result;
        }

        var token = GenerateJwtToken(registerUser);

        return new AuthenticationResult() { Token = token };
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

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var securityToken = handler.CreateToken(tokenDescriptor);
        
        return handler.WriteToken(securityToken);
    }
}