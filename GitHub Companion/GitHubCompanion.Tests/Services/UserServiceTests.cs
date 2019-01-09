using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace GitHubCompanion.Services
{
    public class UserServiceTests
    {
        private readonly ITestOutputHelper output;

        public UserServiceTests(ITestOutputHelper output)
        {
            this.output = output;
        }


#if DEBUG
        [Fact]
#else
        [Fact(Skip = "Integration tests are not supported in automated build. Please run manually.")]
#endif
        [Trait("Category", "SkipWhenLiveUnitTesting")]
        public async Task GetUserCommonAsync()
        {
            // Arrange
            UserServiceV4 userService = new UserServiceV4();

            // Act
            var result = await userService.GetCommonAsync();

            // Assert
            Assert.NotNull(result);
            output.WriteLine(result.Response.ToString());
        }

    }
}