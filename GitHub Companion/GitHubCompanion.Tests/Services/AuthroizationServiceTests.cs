using GitHubCompanion.Models;
using GitHubCompanion.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GitHubCompanion.Tests.Services
{
    public class AuthroizationServiceTests
    {
        [Theory]
        [MemberData(nameof(DataSource.AuthorizeTestData), MemberType = typeof(DataSource))]
        public async Task CreateAuthorizations(int currentRow, string username, string password)
        {
            AuthorizationService service = new AuthorizationService();
            GitHubResponse<Authorization> result = await service.CreateAuthorizationAsync(
                username,
                password,
                parameters: new AuthorizeParameters()
                {
                    Scopes = new List<string>() { "user", "repo" },
                    Note = $"Test Auth" + currentRow,
                    FingerPrint = currentRow.ToString()
                });

            Assert.NotNull(result);
            Assert.NotNull(result.Headers);
            Assert.NotNull(result.Headers.GitHubOptionHeader);
            Assert.False(result.Headers.GitHubOptionHeader.IsRequired);
        }
    }
}
