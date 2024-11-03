using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Services.Model.Filters;
using ActionCommandGame.Services.Model.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActionCommandGame.Api.Controllers
{
    [Authorize]
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
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PlayerCreate playerCreate)
        {
            var result = await _playerService.Create(playerCreate);
            return Ok(result);
        }
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var result = await _playerService.Delete(id);
            return Ok(result);
        }
    }
}
