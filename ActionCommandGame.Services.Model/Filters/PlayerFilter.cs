namespace ActionCommandGame.Services.Model.Filters
{
    public class PlayerFilter
    {
        //Will only show players of the current user
        public bool? FilterUserPlayers { get; set; }
        public string? UserId { get; set; }
    }
}
