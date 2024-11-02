namespace ActionCommandGame.Services.Model.Authentication;

public interface IBearerTokenStore
{
    public string GetToken();
    public void SetToken(string token);
}