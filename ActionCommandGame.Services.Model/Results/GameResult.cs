using ActionCommandGame.Services.Model.Core;

namespace ActionCommandGame.Services.Model.Results
{
    public class GameResult
    {
        public PlayerResult? Player { get; set; }
        public PositiveGameEventResult? PositiveGameEvent { get; set; }
        public NegativeGameEventResult? NegativeGameEvent { get; set; }
        public IList<ServiceMessage> NegativeGameEventMessages { get; set; } = new List<ServiceMessage>();
    }
}
