using ActionCommandGame.Model;
using ActionCommandGame.Repository;
using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Services.Extensions;
using ActionCommandGame.Services.Extensions.Filters;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Filters;
using ActionCommandGame.Services.Model.Requests;
using ActionCommandGame.Services.Model.Results;
using Microsoft.EntityFrameworkCore;

namespace ActionCommandGame.Services
{
    public class PlayerService: IPlayerService
    {
        private readonly ActionCommandGameDbContext _database;

        public PlayerService(ActionCommandGameDbContext database)
        {
            _database = database;
        }

        public async Task<ServiceResult<PlayerResult>> Get(int id)
        {
            var player = await _database.Players
                .ProjectToResult()
                .SingleOrDefaultAsync(p => p.Id == id);

            return new ServiceResult<PlayerResult>(player);
        }

        public async Task<ServiceResult<IList<PlayerResult>>> Find(PlayerFilter? filter)
        {
            var players = await _database.Players
                .ApplyFilter(filter)
                .ProjectToResult()
                .ToListAsync();
            
            return new ServiceResult<IList<PlayerResult>>(players);
        }
        
        public async Task<ServiceResult<PlayerResult>> Create(PlayerCreate playerCreate)
        {
            var player = new Player
            {
                ApplicationUserId = playerCreate.UserId,
                Name = playerCreate.Name,
                Money = 100,
                Experience = 0
            };

            _database.Players.Add(player);
            await _database.SaveChangesAsync();
            
            var playerResults = await _database.Players
                .ProjectToResult()
                .SingleOrDefaultAsync(p => p.Id == player.Id);
            
            return new ServiceResult<PlayerResult>(playerResults);
        }
        
        public async Task<ServiceResult<PlayerResult>> Delete(int id)
        {
            var player = await _database.Players
                .SingleOrDefaultAsync(p => p.Id == id);

            if (player is null)
            {
                return new ServiceResult<PlayerResult>().NotFound();
            }

            _database.Players.Remove(player);
            await _database.SaveChangesAsync();
            
            return new ServiceResult<PlayerResult>( );
        }
    }
}
