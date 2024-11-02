using ActionCommandGame.Configuration;
using ActionCommandGame.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActionCommandGame.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController(IGameService gameService, AppSettings appSettings) : ControllerBase
    {
        // ServiceResult<GameResult> PerformAction(int playerId);
        // ServiceResult<BuyResult> Buy(int playerId, int itemId);
        private readonly IGameService _gameService = gameService;
        private readonly AppSettings _appSettings = appSettings;
        
        [HttpGet("action/{playerId:int}")]
        public async Task<IActionResult> PerformAction(int playerId)
        {
            var result = await _gameService.PerformAction(playerId);
            return Ok(result);
        }
        
        [HttpGet("buy/{playerId:int}/{itemId:int}")]
        public async Task<IActionResult> Buy(int playerId, int itemId)
        {
            var result = await _gameService.Buy(playerId, itemId);
            return Ok(result);
        }
        
        [HttpGet("settings")]
        public IActionResult GetSettings()
        {
            return Ok(_appSettings);
        }
    }
}
