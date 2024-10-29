using ActionCommandGame.Abstractions;

namespace ActionCommandGame.Services.Model.Results
{
    public class NegativeGameEventResult: IHasProbability
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
