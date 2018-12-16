using System.Net;
using System.Threading.Tasks;

namespace GitHubCompanion.Services
{
    public class VariousService : IVariousService
    {
        public async Task<string> GetZenAsync()
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, "Git-Hub-Companion");
                string zen = await client.DownloadStringTaskAsync("https://api.github.com/zen");
                return zen;
            }
        }
    }
}
