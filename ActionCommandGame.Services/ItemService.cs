using ActionCommandGame.Repository;
using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Services.Extensions;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;
using Microsoft.EntityFrameworkCore;

namespace ActionCommandGame.Services
{
    public class ItemService: IItemService
    {
        private readonly ActionCommandGameDbContext _dbContext;

        public ItemService(ActionCommandGameDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ServiceResult<ItemResult>> Get(int id)
        {
            var item = await _dbContext.Items
                .ProjectToResult()
                .SingleOrDefaultAsync(i => i.Id == id);

            return new ServiceResult<ItemResult>(item);
        }

        public async Task<ServiceResult<IList<ItemResult>>> Find()
        {
            var items = await _dbContext.Items
                .ProjectToResult()
                .ToListAsync();

            return new ServiceResult<IList<ItemResult>>(items);
        }
    }
}
