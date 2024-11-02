using ActionCommandGame.Sdk.Extensions;
using ActionCommandGame.Services.Model.Authentication;

namespace ActionCommandGame.Sdk.Handlers;

public class AuthorizationHandler(IBearerTokenStore tokenStore): DelegatingHandler
{
    private readonly IBearerTokenStore _tokenStore = tokenStore;
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = _tokenStore.GetToken();
        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.AddAuthorization(token);
        }
        
        return await base.SendAsync(request, cancellationToken);
    }
}