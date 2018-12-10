using GitHubCompanion.Services;
using System.Threading.Tasks;
using Xunit;

namespace GitHubCompanion.Tests.Services
{
    public class VariousServiceTests
    {

        [Fact]
        public async Task GetZen_ShouldReturnZen()
        {
            VariousService variousService = new VariousService();
            string zen = await variousService.GetZenAsync();
            Assert.NotNull(zen);
        }
    }
}
