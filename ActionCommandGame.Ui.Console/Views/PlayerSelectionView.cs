using ActionCommandGame.Model;
using ActionCommandGame.Sdk;
using ActionCommandGame.Services.Model.Filters;
using ActionCommandGame.Ui.ConsoleApp.Abstractions;
using ActionCommandGame.Ui.ConsoleApp.ConsoleWriters;
using ActionCommandGame.Ui.ConsoleApp.Navigation;
using ActionCommandGame.Ui.ConsoleApp.Stores;
using Microsoft.AspNetCore.Identity;

namespace ActionCommandGame.Ui.ConsoleApp.Views
{
    internal class PlayerSelectionView: IView
    {
        private readonly MemoryStore _memoryStore;
        private readonly PlayerSdk _playerService;
        private readonly NavigationManager _navigationManager;

        public PlayerSelectionView(
            MemoryStore memoryStore,
            PlayerSdk playerService,
            NavigationManager navigationManager)
        {
            _memoryStore = memoryStore;
            _playerService = playerService;
            _navigationManager = navigationManager;
        }

        public async Task Show()
        {
            var playerId = await ReadPlayerId();

            if (!playerId.HasValue)
            {
                ConsoleBlockWriter.Write("Could not load players.", 1, ConsoleColor.Red);
                await _navigationManager.NavigateTo<ExitView>(false);
                return;
            }

            _memoryStore.CurrentPlayerId = playerId.Value;

           
            await _navigationManager.NavigateTo<GameView>();
        }

        private async Task<int?> ReadPlayerId()
        {
            var filter = new PlayerFilter { FilterUserPlayers = true , UserId = _memoryStore.CurrentUserId};
            var players = await _playerService.Find(filter);

            if (players.Data is null || !players.Data.Any())
            {
                await _navigationManager.NavigateTo<CreatePlayerView>();
            }

            var playerTextLines = players.Data.Select(p => $"{p.Id}. {p.Name}").ToList();
            playerTextLines.Add("");
            playerTextLines.Add("Choose your Player by typing the correct number.");
            playerTextLines.Add("Type 'new' to create a new player.");
            playerTextLines.Add("Type 'Del + PlayerId' to delete a player.");
            playerTextLines.Add("Type 'logout' to logout.");
            ConsoleBlockWriter.Write(playerTextLines, 1, ConsoleColor.White);
            ConsoleWriter.WriteText("Your player: ", ConsoleColor.White, false);
            var playerInput = Console.ReadLine();
            
            if(playerInput.ToLower() == "new")
            {
                await _navigationManager.NavigateTo<CreatePlayerView>();
            }
            
            
            if(playerInput.ToLower().StartsWith("del"))
            {
                var Id = playerInput.Split(" ")[1];
                await _playerService.Delete(int.Parse(Id));
                return await ReadPlayerId();
            }
            
            if(playerInput.ToLower() == "logout")
            {
                _memoryStore.CurrentUserId = null;
                await _navigationManager.NavigateTo<SignInView>();
            }
            
            if (!int.TryParse(playerInput, out int playerId))
            {
                Console.Clear();

                ConsoleWriter.WriteText("Please enter a correct player number.", ConsoleColor.Blue);

                return await ReadPlayerId();
            }

            return playerId;
        }
    }
}
