namespace ActionCommandGame.Services.Model.Requests
{
    public class UserSignInRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
