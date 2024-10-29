using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Filters;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Services.Abstractions
{
    public interface IPlayerService
    {
        Task<ServiceResult<PlayerResult>> Get(int id);
        Task<ServiceResult<IList<PlayerResult>>> Find(PlayerFilter? filter);
    }
}
