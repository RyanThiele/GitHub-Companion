using System.Net.Http;

namespace GitHubCompanion.Services
{
    /// <summary>
    /// A base service class for all GitHub API v3 services.
    /// </summary>
    public abstract class GitHubServiceV3Base
    {
        protected HttpClient CreateHttpClient()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("User-Agent", "Git-Hub-Companion");
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.full+json");
            return client;
        }
    }
}