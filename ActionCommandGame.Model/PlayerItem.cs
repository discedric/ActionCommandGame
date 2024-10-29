using ActionCommandGame.Abstractions;

namespace ActionCommandGame.Model
{
    public class PlayerItem: IIdentifiable
    {

        public int Id { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; } = null!;

        public int ItemId { get; set; }
        public Item Item { get; set; } = null!;

        public int RemainingFuel { get; set; }
        public int RemainingAttack { get; set; }
        public int RemainingDefense { get; set; }

        public IList<Player> FuelPlayers { get; set; } = new List<Player>();
        public IList<Player> AttackPlayers { get; set; } = new List<Player>();
        public IList<Player> DefensePlayers { get; set; } = new List<Player>();
    }
}
