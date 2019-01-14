using GitHubCompanion.Models;
using GitHubCompanion.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace GitHubCompanion.ViewModels
{
    public class HomeViewModelTests
    {
        [Fact]
        public async Task NoToken_LoginShouldBeAvailable()
        {
            // prepare
            var settingsService = new Mock<ISettingsService>();
            settingsService.Setup(x => x.GetTokenAsync(TokenTypes.AuthorizationToken))
                .Returns(Task.FromResult(new GitHubToken(string.Empty, TokenTypes.AuthorizationToken)));

            HomeViewModel viewModel = new HomeViewModel(new Mock<ILogger<HomeViewModel>>().Object,
                new Mock<INavigationService>().Object,
                settingsService.Object,
                new Mock<IAuthorizationService>().Object,
                new Mock<IProfileService>().Object);

            // act
            await viewModel.PrepareViewModelAsync();

            // assert
            Assert.False(viewModel.IsAuthenticated);
            Assert.True(viewModel.LoginWithCredentialsCommand.CanExecute(null));
            Assert.True(viewModel.LoginWithTokenCommand.CanExecute(null));
        }

        [Fact]
        public async Task InvalidAuthorizationToken_TokenShouldClear_LoginShouldBeAvailable()
        {
            // prepare
            bool isClearTokenCalled = false;
            var settingsService = new Mock<ISettingsService>();
            var authorizationService = new Mock<IAuthorizationService>();

            settingsService.Setup(x => x.GetTokenAsync(TokenTypes.AuthorizationToken))
                .Returns(Task.FromResult(new GitHubToken("I am a token", TokenTypes.AuthorizationToken)));

            settingsService.Setup(x => x.ClearTokenAsync(TokenTypes.AuthorizationToken))
                .Callback(() => { isClearTokenCalled = true; })
                .Returns(Task.FromResult(true));

            authorizationService.Setup(x => x.AuthenticateWithTokenAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new AuthenticationResult() { AuthenticationSuccessful = false }));

            HomeViewModel viewModel = new HomeViewModel(new Mock<ILogger<HomeViewModel>>().Object,
                new Mock<INavigationService>().Object,
                settingsService.Object,
                authorizationService.Object,
                new Mock<IProfileService>().Object);

            // act
            await viewModel.PrepareViewModelAsync();
            viewModel.SignOutCommand.Execute();

            // assert
            Assert.True(isClearTokenCalled, "Clear token must be called.");
            Assert.False(viewModel.IsAuthenticated);
            Assert.True(viewModel.LoginWithCredentialsCommand.CanExecute(null));
            Assert.True(viewModel.LoginWithTokenCommand.CanExecute(null));
        }

        [Fact]
        public async Task InvalidPersonalAccessToken_TokenShouldClear_LoginShouldBeAvailable()
        {
            // prepare
            bool isClearTokenCalled = false;
            var settingsService = new Mock<ISettingsService>();
            var authorizationService = new Mock<IAuthorizationService>();

            settingsService.Setup(x => x.GetTokenAsync(TokenTypes.PersonalAccessToken))
                .Returns(Task.FromResult(new GitHubToken("I am a token", TokenTypes.PersonalAccessToken)));

            settingsService.Setup(x => x.ClearTokenAsync(TokenTypes.PersonalAccessToken))
                .Callback(() => { isClearTokenCalled = true; })
                .Returns(Task.FromResult(true));

            authorizationService.Setup(x => x.AuthenticateWithPersonalAccessTokenAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new AuthenticationResult() { AuthenticationSuccessful = false }));

            HomeViewModel viewModel = new HomeViewModel(new Mock<ILogger<HomeViewModel>>().Object,
                new Mock<INavigationService>().Object,
                settingsService.Object,
                authorizationService.Object,
                new Mock<IProfileService>().Object);

            // act
            await viewModel.PrepareViewModelAsync();
            viewModel.SignOutCommand.Execute();

            // assert
            Assert.True(isClearTokenCalled, "Clear token must be called.");
            Assert.False(viewModel.IsAuthenticated);
            Assert.True(viewModel.LoginWithCredentialsCommand.CanExecute(null));
            Assert.True(viewModel.LoginWithTokenCommand.CanExecute(null));
        }

        [Fact]
        public async Task ValidAuthorizationToken_TokenShouldSet_LoginShouldBeUnAvailable_SignoutShoudBeAvailable()
        {
            // prepare
            GitHubToken token = new GitHubToken("I am a token", TokenTypes.AuthorizationToken);
            var settingsService = new Mock<ISettingsService>();
            var authorizationService = new Mock<IAuthorizationService>();

            settingsService.Setup(x => x.GetTokenAsync(TokenTypes.AuthorizationToken))
                .Returns(Task.FromResult(token));

            authorizationService.Setup(x => x.AuthenticateWithTokenAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new AuthenticationResult() { AuthenticationSuccessful = true }));

            HomeViewModel viewModel = new HomeViewModel(new Mock<ILogger<HomeViewModel>>().Object,
                new Mock<INavigationService>().Object,
                settingsService.Object,
                authorizationService.Object,
                new Mock<IProfileService>().Object);

            // act
            await viewModel.PrepareViewModelAsync();

            // assert
            Assert.True(viewModel.IsAuthenticated);
            Assert.False(viewModel.LoginWithCredentialsCommand.CanExecute(null));
            Assert.False(viewModel.LoginWithTokenCommand.CanExecute(null));
        }

        [Fact]
        public async Task ValidPersonalAccessToken_TokenShouldSet_LoginShouldBeUnAvailable_SignoutShoudBeAvailable()
        {
            // prepare
            GitHubToken token = new GitHubToken("I am a token", TokenTypes.PersonalAccessToken);

            var settingsService = new Mock<ISettingsService>();
            var authorizationService = new Mock<IAuthorizationService>();
            var profileService = new Mock<IProfileService>();

            settingsService.Setup(x => x.GetTokenAsync(TokenTypes.PersonalAccessToken))
                .Returns(Task.FromResult(token));

            authorizationService.Setup(x => x.AuthenticateWithPersonalAccessTokenAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new AuthenticationResult() { AuthenticationSuccessful = true }));

            profileService.Setup(x => x.GetSelfProfileAsync(token.Token))
               .Returns(Task.FromResult(new GitHubResponse<Profile>()));

            HomeViewModel viewModel = new HomeViewModel(new Mock<ILogger<HomeViewModel>>().Object,
                new Mock<INavigationService>().Object,
                settingsService.Object,
                authorizationService.Object,
                new Mock<IProfileService>().Object);

            // act
            await viewModel.PrepareViewModelAsync();

            // assert
            Assert.True(viewModel.IsAuthenticated);
            Assert.False(viewModel.LoginWithCredentialsCommand.CanExecute(null));
            Assert.False(viewModel.LoginWithTokenCommand.CanExecute(null));
        }

    }
}
