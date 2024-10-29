using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Services.Model.Results;
using ActionCommandGame.Ui.ConsoleApp.Abstractions;
using ActionCommandGame.Ui.ConsoleApp.ConsoleWriters;

namespace ActionCommandGame.Ui.ConsoleApp.Views
{
    internal class ShopView: IView
    {
        private readonly IItemService _itemService;

        public ShopView(IItemService itemService)
        {
            _itemService = itemService;
        }
        public async Task Show()
        {
            ConsoleBlockWriter.Write("Shop");
            ConsoleWriter.WriteText("Available Shop Items", ConsoleColor.Green);
            var shopItems = await _itemService.Find();

            if (shopItems.Data is null)
            {
                return;
            }

            foreach (var item in shopItems.Data)
            {
                ShowItem(item);
            }
            ConsoleWriter.WriteText();
        }

        private static void ShowItem(ItemResult item)
        {
            ConsoleWriter.WriteText($"\t[{item.Id}] {item.Name} €{item.Price}", ConsoleColor.White);
            if (!string.IsNullOrWhiteSpace(item.Description))
            {
                ConsoleWriter.WriteText($"\t\t{item.Description}");
            }
            if (item.Fuel > 0)
            {
                ConsoleWriter.WriteText("\t\tFuel: ", ConsoleColor.White, false);
                ConsoleWriter.WriteText($"{item.Fuel}");
            }
            if (item.Attack > 0)
            {
                ConsoleWriter.WriteText("\t\tAttack: ", ConsoleColor.White, false);
                ConsoleWriter.WriteText($"{item.Attack}");
            }
            if (item.Defense > 0)
            {
                ConsoleWriter.WriteText("\t\tDefense: ", ConsoleColor.White, false);
                ConsoleWriter.WriteText($"{item.Defense}");
            }
            if (item.ActionCooldownSeconds > 0)
            {
                ConsoleWriter.WriteText("\t\tCooldown seconds: ", ConsoleColor.White, false);
                ConsoleWriter.WriteText($"{item.ActionCooldownSeconds}");
            }
        }
    }
}
