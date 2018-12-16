using GitHubCompanion.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace GitHubCompanion.Services
{
    public class ProfileService : GitHubServiceV3Base, IProfileService
    {

        public async Task<GitHubResponse<Profile>> GetSelfProfileAsync(string token)
        {
            GitHubResponse<Profile> result = new GitHubResponse<Profile>();

            using (HttpClient client = CreateHttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"https://api.github.com/user");
                result.Headers = new Models.Headers.GitHubHeaders(response.Headers);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result.Response = await response.Content.ReadAsAsync<Profile>();
                }

                return result;
            }
        }

        public async Task<GitHubResponse<Profile>> GetUserProfileAsync(string username)
        {
            GitHubResponse<Profile> result = new GitHubResponse<Profile>();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Git-Hub-Companion");
                HttpResponseMessage response = await client.GetAsync($"https://api.github.com/users/{username}");
                result.Headers = new Models.Headers.GitHubHeaders(response.Headers);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result.Response = await response.Content.ReadAsAsync<Profile>();
                }

                return result;
            }
        }
    }
}
