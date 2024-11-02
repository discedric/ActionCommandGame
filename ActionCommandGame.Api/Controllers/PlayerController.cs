using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Services.Model.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActionCommandGame.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController(IPlayerService playerService) : ControllerBase
    {
        private readonly IPlayerService _playerService = playerService;
        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PlayerFilter? filter)
        {
            var result = await _playerService.Find(filter);
            return Ok(result);
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var result = await _playerService.Get(id);
            return Ok(result);
        }
    }
}
