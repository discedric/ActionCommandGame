using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Services.Abstractions
{
    public interface IGameService
    {
        Task<ServiceResult<GameResult>> PerformAction(int playerId);
        Task<ServiceResult<BuyResult>> Buy(int playerId, int itemId);
    }
}
