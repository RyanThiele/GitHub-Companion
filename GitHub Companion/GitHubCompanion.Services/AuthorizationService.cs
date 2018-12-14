using GitHubCompanion.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GitHubCompanion.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        public async Task<AuthenticationResult> AuthenticateAsync(string username, string password, string tfaCode)
        {
            AuthenticationResult result = new AuthenticationResult();

            using (HttpClient client = new HttpClient())
            {
                // create a basic auth header.
                byte[] authroizationHeader = Encoding.ASCII.GetBytes($"{username}:{password}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(authroizationHeader));
                client.DefaultRequestHeaders.Add("User-Agent", "Git-Hub-Companion");

                // do we have a tfa code?
                if (!String.IsNullOrWhiteSpace(tfaCode)) client.DefaultRequestHeaders.Add("X-GitHub-OTP", tfaCode);

                // post content.
                HttpResponseMessage responseMessage = await client.GetAsync("https://api.github.com");
                if (responseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Models.Headers.GitHubHeaders headers = new Models.Headers.GitHubHeaders(responseMessage.Headers);
                    return new AuthenticationResult() { OptionHeader = headers.GitHubOptionHeader };
                }

                responseMessage.EnsureSuccessStatusCode();
                return new AuthenticationResult() { AuthentictionSuccessful = true };
            }

        }

        public async Task<GitHubResponse<Authorization>> CreateAuthorizationAsync(string username, string password, int? TwoFactorAuthorizationCode = null, AuthorizeParameters parameters = null)
        {
            GitHubResponse<Authorization> response = new GitHubResponse<Authorization>();

            using (HttpClient client = new HttpClient())
            {
                // create a basic auth header.
                byte[] authroizationHeader = Encoding.ASCII.GetBytes($"{username}:{password}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(authroizationHeader));
                client.DefaultRequestHeaders.Add("User-Agent", "Git-Hub-Companion");

                if (TwoFactorAuthorizationCode.HasValue) client.DefaultRequestHeaders.Add("X-GitHub-OTP", TwoFactorAuthorizationCode.Value.ToString());

                // post content.
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync("https://api.github.com/authorizations", parameters);
                if (responseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    response.Headers = new Models.Headers.GitHubHeaders(responseMessage.Headers);
                    return response;
                }

                responseMessage.EnsureSuccessStatusCode();
                response.Response = await responseMessage.Content.ReadAsAsync<Authorization>();
                return response;
            }

        }

        Task<bool> IAuthorizationService.AuthorizeWithTokenAsync(string token)
        {
            throw new NotImplementedException();
        }
    }
}
