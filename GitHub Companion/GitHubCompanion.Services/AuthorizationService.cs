using GitHubCompanion.Models;
using GitHubCompanion.Models.Headers;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GitHubCompanion.Services
{
    public class AuthorizationService : GitHubServiceV3Base, IAuthorizationService
    {
        public async Task<AuthenticationResult> AuthenticateAsync(string username, string password, string tfaCode)
        {
            AuthenticationResult result = new AuthenticationResult();

            using (HttpClient client = CreateHttpClient())
            {
                // create a basic auth header.
                byte[] authroizationHeader = Encoding.ASCII.GetBytes($"{username}:{password}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authroizationHeader));

                // do we have a tfa code?
                if (!String.IsNullOrWhiteSpace(tfaCode)) client.DefaultRequestHeaders.Add("X-GitHub-OTP", tfaCode);

                // get content.
                HttpResponseMessage responseMessage = await client.GetAsync("https://api.github.com");
                if (responseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    GitHubHeaders headers = new GitHubHeaders(responseMessage.Headers);
                    return new AuthenticationResult() { OptionHeader = headers.GitHubOptionHeader };
                }

                responseMessage.EnsureSuccessStatusCode();
                return new AuthenticationResult() { AuthenticationSuccessful = true };
            }

        }

        public async Task<AuthenticationResult> AuthenticateWithPersonalAccessTokenAsync(string personalAccessToken)
        {
            AuthenticationResult result = new AuthenticationResult();

            using (HttpClient client = CreateHttpClient())
            {
                // create a basic auth header.
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", personalAccessToken);


                // get content.
                HttpResponseMessage responseMessage = await client.GetAsync("https://api.github.com");
                if (responseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Models.Headers.GitHubHeaders headers = new Models.Headers.GitHubHeaders(responseMessage.Headers);
                    return new AuthenticationResult() { OptionHeader = headers.GitHubOptionHeader };
                }

                responseMessage.EnsureSuccessStatusCode();
                return new AuthenticationResult() { AuthenticationSuccessful = true };
            }
        }

        public async Task<AuthenticationResult> AuthenticateWithTokenAsync(string token)
        {
            AuthenticationResult result = new AuthenticationResult();

            using (HttpClient client = CreateHttpClient())
            {
                // create a basic auth header.
                client.DefaultRequestHeaders.Add("token", token);


                // get content.
                HttpResponseMessage responseMessage = await client.GetAsync("https://api.github.com");
                if (responseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Models.Headers.GitHubHeaders headers = new Models.Headers.GitHubHeaders(responseMessage.Headers);
                    return new AuthenticationResult() { OptionHeader = headers.GitHubOptionHeader };
                }

                responseMessage.EnsureSuccessStatusCode();
                return new AuthenticationResult() { AuthenticationSuccessful = true };
            }

        }

        public async Task<GitHubResponse<Authorization>> CreateAuthorizationTokenForAppAsync(AuthorizeParameters parameters, string username, string password, string twoFactorAuthorizationCode = null)
        {
            GitHubResponse<Authorization> response = new GitHubResponse<Authorization>();

            using (HttpClient client = CreateHttpClient())
            {
                // create a basic auth header.
                byte[] authroizationHeader = Encoding.ASCII.GetBytes($"{username}:{password}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authroizationHeader));

                if (!String.IsNullOrWhiteSpace(twoFactorAuthorizationCode)) client.DefaultRequestHeaders.Add("X-GitHub-OTP", twoFactorAuthorizationCode);

                string requestUri = $"https://api.github.com/authorizations/clients/{parameters.Client_Id}";
                // post content.
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(requestUri, new
                {
                    client_secret = parameters.Client_Secret,
                    scopes = parameters.Scopes
                });

                if (responseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    response.Headers = new GitHubHeaders(responseMessage.Headers);
                    return response;
                }

                responseMessage.EnsureSuccessStatusCode();
                response.Response = await responseMessage.Content.ReadAsAsync<Authorization>();
                return response;
            }

        }


    }
}
