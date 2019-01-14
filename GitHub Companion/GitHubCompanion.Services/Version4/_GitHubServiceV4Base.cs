using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GitHubCompanion.Services.Version4
{
    /// <summary>
    /// A base service class for all GitHub API v3 services.
    /// </summary>
    public abstract class GitHubServiceV4Base
    {
        protected const string API_ENDPOINT = "https://api.github.com/graphql";

        protected HttpClient CreateHttpClient(string token = null)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("User-Agent", "Git-Hub-Companion");
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.full+json");

            if (!String.IsNullOrWhiteSpace(token)) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            return client;
        }
    }
}