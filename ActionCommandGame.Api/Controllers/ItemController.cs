using ActionCommandGame.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActionCommandGame.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController(IItemService itemService) : ControllerBase
    {
        // ServiceResult<IList<ItemResult>> Find();
        private readonly IItemService _itemService = itemService;
        
        [HttpGet]
        public async Task<IActionResult> Find()
        {
            var result = await _itemService.Find();
            return Ok(result);
        }
    }
}
