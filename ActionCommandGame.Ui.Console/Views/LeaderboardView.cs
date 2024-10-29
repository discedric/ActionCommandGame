
using ActionCommandGame.Extensions;
using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Services.Model.Filters;
using ActionCommandGame.Ui.ConsoleApp.Abstractions;
using ActionCommandGame.Ui.ConsoleApp.ConsoleWriters;
using ActionCommandGame.Ui.ConsoleApp.Stores;

namespace ActionCommandGame.Ui.ConsoleApp.Views
{
    internal class LeaderboardView: IView
    {
        private readonly MemoryStore _memoryStore;
        private readonly IPlayerService _playerService;

        public LeaderboardView(
            MemoryStore memoryStore,
            IPlayerService playerService)
        {
            _memoryStore = memoryStore;
            _playerService = playerService;
        }

        public async Task Show()
        {
            ConsoleBlockWriter.Write("Leaderboard");

            var playersResult = await _playerService.Find(new PlayerFilter());

            if (playersResult.Data is null)
            {
                return;
            }

            var orderedPlayers = playersResult.Data.OrderByDescending(p => p.Experience).ToList();

            foreach (var player in orderedPlayers)
            {
                var text = $"\tLevel {player.GetLevel()} {player.Name} ({player.Experience})";
                if (player.Id == _memoryStore.CurrentPlayerId)
                {
                    ConsoleWriter.WriteText(text, ConsoleColor.Yellow);
                }
                else
                {
                    ConsoleWriter.WriteText(text);
                }
            }
        }
    }
}
