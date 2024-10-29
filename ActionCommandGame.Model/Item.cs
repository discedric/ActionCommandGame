using ActionCommandGame.Abstractions;

namespace ActionCommandGame.Model
{
    public class Item: IIdentifiable
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        public int Price { get; set; }
        public int Fuel { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int ActionCooldownSeconds { get; set; }

        public IList<PlayerItem> PlayerItems { get; set; } = new List<PlayerItem>();
    }
}
