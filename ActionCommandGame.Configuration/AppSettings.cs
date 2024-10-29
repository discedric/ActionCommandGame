namespace ActionCommandGame.Configuration
{
    public class AppSettings
    {
        public string CommandPromptText { get; set; } = null!;
        public string GameName { get; set; } = null!;
        public string ActionCommand { get; set; } = null!;
        public string ActionText { get; set; } = null!;
        public int DefaultCooldownSeconds { get; set; }
    }
}
