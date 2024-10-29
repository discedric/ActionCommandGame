using ActionCommandGame.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActionCommandGame.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class GameController(IGameService gameService) : ControllerBase
    {
        // ServiceResult<GameResult> PerformAction(int playerId);
        // ServiceResult<BuyResult> Buy(int playerId, int itemId);


    }
}
