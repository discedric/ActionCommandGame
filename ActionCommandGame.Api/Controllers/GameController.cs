using ActionCommandGame.Configuration;
using ActionCommandGame.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActionCommandGame.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GameController(IGameService gameService, AppSettings appSettings) : ControllerBase
    {
        // ServiceResult<GameResult> PerformAction(int playerId);
        // ServiceResult<BuyResult> Buy(int playerId, int itemId);
        private readonly IGameService _gameService = gameService;
        private readonly AppSettings _appSettings = appSettings;
        
        [HttpGet("action/{playerId:int}")]
        public async Task<IActionResult> PerformAction([FromRoute]int playerId)
        {
            var result = await _gameService.PerformAction(playerId);
            return Ok(result);
        }
        
        [HttpGet("buy/{playerId:int}/{itemId:int}")]
        public async Task<IActionResult> Buy([FromRoute]int playerId,[FromRoute] int itemId)
        {
            var result = await _gameService.Buy(playerId, itemId);
            return Ok(result);
        }
        
        [AllowAnonymous]
        [HttpGet("settings")]
        public IActionResult GetSettings()
        {
            return Ok(_appSettings);
        }
    }
}
