using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Filters;
using ActionCommandGame.Services.Model.Requests;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Services.Abstractions
{
    public interface IPlayerService
    {
        Task<ServiceResult<PlayerResult>> Get(int id);
        Task<ServiceResult<IList<PlayerResult>>> Find(PlayerFilter? filter);
        Task<ServiceResult<PlayerResult>> Create(PlayerCreate playerCreate);
        Task<ServiceResult<PlayerResult>> Delete(int id);
    }
}
