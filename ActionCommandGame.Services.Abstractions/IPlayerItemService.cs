using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Filters;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Services.Abstractions
{
    public interface IPlayerItemService
    {
        Task<ServiceResult<PlayerItemResult>> Get(int id);
        Task<ServiceResult<IList<PlayerItemResult>>> Find(PlayerItemFilter filter);
        Task<ServiceResult<PlayerItemResult>> Create(int playerId, int itemId);
        Task<ServiceResult> Delete(int id);
    }
}
