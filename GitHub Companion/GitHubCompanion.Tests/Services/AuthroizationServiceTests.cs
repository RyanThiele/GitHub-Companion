﻿using GitHubCompanion.Models;
using GitHubCompanion.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GitHubCompanion.Tests.Services
{
    public class AuthroizationServiceTests
    {
        [Theory(Skip = "Integration tests are not supported in automated build. PLease run manually.")]
        [MemberData(nameof(DataSource.AuthorizeTestData), MemberType = typeof(DataSource))]
        public async Task CreateAuthorizationTokenForAppAsync(int currentRow, string username, string password)
        {
            AuthorizationService service = new AuthorizationService();
            GitHubResponse<Authorization> result = await service.CreateAuthorizationTokenForAppAsync(
                new AuthorizeParameters()
                {
                    Scopes = new List<string>() { "user", "repo" },
                    Note = $"Test Auth" + currentRow
                },
                username,
                password);


            Assert.NotNull(result);
            Assert.NotNull(result.Headers);
            Assert.NotNull(result.Headers.GitHubOptionHeader);
            Assert.False(result.Headers.GitHubOptionHeader.IsRequired);
        }
    }
}
