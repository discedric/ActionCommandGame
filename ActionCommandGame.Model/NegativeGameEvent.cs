using ActionCommandGame.Abstractions;

namespace ActionCommandGame.Model
{
    public class NegativeGameEvent: IIdentifiable, IHasProbability
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string DefenseWithGearDescription { get; set; }
        public required string DefenseWithoutGearDescription { get; set; }
        public int DefenseLoss { get; set; }
        public int Probability { get; set; }
    }
}
