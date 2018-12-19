using GitHubCompanion.Services;
using System.Threading.Tasks;
using Xunit;

namespace GitHubCompanion.Tests.Services
{
    public class VariousServiceTests
    {

        [Fact(Skip = "Integration tests are not supported in automated build. PLease run manually.")]
        public async Task GetZen_ShouldReturnZen()
        {
            VariousService variousService = new VariousService();
            string zen = await variousService.GetZenAsync();
            Assert.NotNull(zen);
        }
    }
}
