using GitHubCompanion.Models;
using GitHubCompanion.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GitHubCompanion.ViewModels
{
    public class HomeViewModelTests
    {
        [Fact]
        public async Task NoToken_LoginsShouldBeAvailable()
        {
            // prepare
            var settingsService = new Mock<ISettingsService>();
            settingsService.Setup(x => x.GetTokenAsync())
                .Returns(Task.FromResult(String.Empty));

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
        public async Task InvalidToken_TokenShouldClear_LoginsShouldBeAvailable()
        {
            // prepare
            bool isClearTokenCalled = false;
            var settingsService = new Mock<ISettingsService>();
            var authorizationService = new Mock<IAuthorizationService>();

            settingsService.Setup(x => x.GetTokenAsync())
                .Returns(Task.FromResult("I am a token"));

            settingsService.Setup(x => x.ClearTokenAsync()).Callback(() => { isClearTokenCalled = true; }).Returns(Task.FromResult(true));

            authorizationService.Setup(x => x.AuthenticateWithTokenAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new AuthenticationResult() { AuthenticationSuccessful = false }));

            HomeViewModel viewModel = new HomeViewModel(new Mock<ILogger<HomeViewModel>>().Object,
                new Mock<INavigationService>().Object,
                settingsService.Object,
                authorizationService.Object,
                new Mock<IProfileService>().Object);

            // act
            await viewModel.PrepareViewModelAsync();

            // assert
            Assert.True(isClearTokenCalled, "Clear token must be called.");
            Assert.False(viewModel.IsAuthenticated);
            Assert.True(viewModel.LoginWithCredentialsCommand.CanExecute(null));
            Assert.True(viewModel.LoginWithTokenCommand.CanExecute(null));
        }

        [Fact]
        public async Task ValidToken_TokenShouldSet_LoginsShouldBeUnAvailable_SignoutShoudBeAvailable()
        {
            // prepare
            string token = "I am a token";
            var settingsService = new Mock<ISettingsService>();
            var authorizationService = new Mock<IAuthorizationService>();

            settingsService.Setup(x => x.GetTokenAsync())
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
    }
}
