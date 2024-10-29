using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Services.Abstractions
{
    public interface IItemService
    {
        Task<ServiceResult<ItemResult>> Get(int id);
        Task<ServiceResult<IList<ItemResult>>> Find();
    }
}
