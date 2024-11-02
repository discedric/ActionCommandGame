using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Services.Model.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActionCommandGame.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerItemController(IPlayerItemService playerItemService) : ControllerBase
    {
        private readonly IPlayerItemService _playerItemService = playerItemService;
        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PlayerItemFilter filter)
        {
            var result = await _playerItemService.Find(filter);
            return Ok(result);
        }
    }
}
