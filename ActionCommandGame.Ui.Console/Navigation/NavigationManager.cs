using ActionCommandGame.Ui.ConsoleApp.Abstractions;
using ActionCommandGame.Ui.ConsoleApp.ConsoleWriters;

namespace ActionCommandGame.Ui.ConsoleApp.Navigation
{
    internal class NavigationManager
    {
        private readonly IServiceProvider _serviceProvider;

        public NavigationManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task NavigateTo<T>(bool clearScreen = true) where T: IView
        {
            var view = _serviceProvider.GetService(typeof(T)) as IView;

            if (view is null)
            {
                ConsoleWriter.WriteText("Error navigating to view", ConsoleColor.Red);
                return;
            }

            if (clearScreen)
            {
                Console.Clear();
            }
            await view.Show();
        }
    }
}
