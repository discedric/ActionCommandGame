using ActionCommandGame.Services;
using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Services.Model.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActionCommandGame.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController(IIdentityService identityService) : ControllerBase
    {
        private readonly IIdentityService _identityService = identityService;
        
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(UserSignInRequest request)
        {
            var result = await _identityService.SignIn(request);
            return Ok(result);
        }
        
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserRegisterRequest request)
        {
            var result = await _identityService.SignUp(request);
            return Ok(result);
        }
    }
}
