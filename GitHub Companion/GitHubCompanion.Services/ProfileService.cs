﻿using GitHubCompanion.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace GitHubCompanion.Services
{
    public class ProfileService : IProfileService
    {
        public async Task<Profile> GetUserProfileAsync(string username)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Git-Hub-Companion");
                HttpResponseMessage response = await client.GetAsync($"https://api.github.com/users/{username}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Profile>();
            }
        }
    }
}
