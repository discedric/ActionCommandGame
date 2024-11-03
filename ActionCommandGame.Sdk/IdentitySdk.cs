using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ActionCommandGame.Services.Model.Requests;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Sdk
{
    public class IdentitySdk(IHttpClientFactory httpClientFactory)
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public async Task<AuthenticationResult> SignIn(UserSignInRequest userSignInRequest)
        {
            var client = _httpClientFactory.CreateClient("ActionCommandGame");
            var response = await client.PostAsJsonAsync("api/identity/signin", userSignInRequest);

            response.EnsureSuccessStatusCode();
            
            var result = await response.Content.ReadFromJsonAsync<AuthenticationResult>();
            if (result is null)
            {
                return new AuthenticationResult();
            }

            return result;
        }

        public async Task<AuthenticationResult> SignUp(UserRegisterRequest userRegisterRequest)
        {
            var client = _httpClientFactory.CreateClient("ActionCommandGame");
            var response = await client.PostAsJsonAsync("api/identity/signup", userRegisterRequest);
            response.EnsureSuccessStatusCode();
            
            var result = await response.Content.ReadFromJsonAsync<AuthenticationResult>();
            if (result is null)
            {
                return new AuthenticationResult();
            }

            return result;
        }
    }
}
