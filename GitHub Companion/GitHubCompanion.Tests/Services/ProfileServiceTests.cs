using GitHubCompanion.Models;
using GitHubCompanion.Services;
using System.Threading.Tasks;
using Xunit;

namespace GitHubCompanion.Tests.Services
{
    public class ProfileServiceTests
    {
        [Fact]
        public async Task GetUserProfileTest()
        {
            ProfileService service = new ProfileService();
            GitHubResponse<Profile> response = await service.GetUserProfileAsync("RyanThiele");
            Assert.NotNull(response);
            Assert.NotNull(response.Response);
        }
    }
}
