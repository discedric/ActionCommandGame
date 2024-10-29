namespace ActionCommandGame.Services.Model.Core
{
    public class ServiceMessage
    {
        public required string Code { get; set; }
        public required string Message { get; set; }
        public MessagePriority MessagePriority { get; set; }
    }
}
