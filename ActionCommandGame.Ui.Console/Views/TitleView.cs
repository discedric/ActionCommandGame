using ActionCommandGame.Configuration;
using ActionCommandGame.Ui.ConsoleApp.Abstractions;
using ActionCommandGame.Ui.ConsoleApp.ConsoleWriters;
using ActionCommandGame.Ui.ConsoleApp.Navigation;

namespace ActionCommandGame.Ui.ConsoleApp.Views
{
    internal class TitleView: IView
    {
        private readonly AppSettings _appSettings;
        private readonly NavigationManager _navigationManager;

        public TitleView(
            AppSettings appSettings,
            NavigationManager navigationManager)
        {
            _appSettings = appSettings;
            _navigationManager = navigationManager;
        }

        public async Task Show()
        {
            var titleLines = new List<string>
            {
                "Vives Development Studios",
                "presents",
                "",
                _appSettings.GameName,
                "",
                "\"An amazing adventure - 87%\" - any gaming magazine"
            };

            ConsoleBlockWriter.Write(titleLines, 4, ConsoleColor.Blue);

            ConsoleWriter.WriteText("Press any key to continue. Now where's the \"Any\" key?");
            Console.ReadLine();

            await _navigationManager.NavigateTo<PlayerSelectionView>();
        }
    }
}
