using GitHubCompanion.Models;
using GitHubCompanion.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace GitHubCompanion.ViewModels
{
    public class LoginWithCredentialsViewModelTests
    {
        [Fact]
        public void AttemptLoginWithEmptyUsername_ShouldNotExecute()
        {
            // Prepare
            var logger = new Mock<ILogger<ViewModels.LoginWithCredentialsViewModel>>().Object;
            var navigationService = new Mock<INavigationService>().Object;
            var authorizationService = new Mock<IAuthorizationService>().Object;
            var settingsService = new Mock<ISettingsService>().Object;

            LoginWithCredentialsViewModel loginViewModel = new ViewModels.LoginWithCredentialsViewModel();
            loginViewModel.Username = null;
            loginViewModel.Password = "A Password";

            // Act
            var canExecute = loginViewModel.LoginCommand.CanExecute(null);

            // Assert
            Assert.False(canExecute);
        }

        [Fact]
        public void AttemptLoginWithEmptyPassword_ShouldNotExecute()
        {
            // Prepare
            var logger = new Mock<ILogger<ViewModels.LoginWithCredentialsViewModel>>().Object;
            var navigationService = new Mock<INavigationService>().Object;
            var authorizationService = new Mock<IAuthorizationService>().Object;
            var settingsService = new Mock<ISettingsService>().Object;

            LoginWithCredentialsViewModel loginViewModel = new ViewModels.LoginWithCredentialsViewModel();
            loginViewModel.Username = "A Username";
            loginViewModel.Password = null;

            // Act
            var canExecute = loginViewModel.LoginCommand.CanExecute(null);

            // Assert
            Assert.False(canExecute);
        }

        [Fact]
        public void AttemptLoginWithUsernameAndPassword_ShouldExecute()
        {
            // Prepare
            var logger = new Mock<ILogger<ViewModels.LoginWithCredentialsViewModel>>().Object;
            var navigationService = new Mock<INavigationService>().Object;
            var authorizationService = new Mock<IAuthorizationService>().Object;
            var settingsService = new Mock<ISettingsService>().Object;

            LoginWithCredentialsViewModel loginViewModel = new ViewModels.LoginWithCredentialsViewModel();
            loginViewModel.Username = "A Username";
            loginViewModel.Password = "A Password";

            // Act
            var canExecute = loginViewModel.LoginCommand.CanExecute(null);

            // Assert
            Assert.True(canExecute);
        }

        [Fact]
        public void ServiceReturnedDoesNotNeedsAutoCode_ShouldNotShowAuthCode()
        {
            // Prepare
            var logger = new Mock<ILogger<ViewModels.LoginWithCredentialsViewModel>>().Object;
            var navigationService = new Mock<INavigationService>().Object;
            var authorizationService = new Mock<IAuthorizationService>();
            authorizationService.Setup(x => x.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>(), null))
                .Returns(Task.FromResult(new AuthenticationResult()
                {
                    AuthenticationSuccessful = false,
                    OptionHeader = new Models.Headers.GitHubOptionHeader() { IsRequired = false, Type = Models.Headers.GitHubOptionHeaderType.Unknown }
                }));
            var settingsService = new Mock<ISettingsService>().Object;

            LoginWithCredentialsViewModel loginViewModel = new LoginWithCredentialsViewModel(logger, navigationService, authorizationService.Object, settingsService);
            loginViewModel.Username = "A Username";
            loginViewModel.Password = "A Password";

            // Act
            var canExecute = loginViewModel.LoginCommand.CanExecute(null);
            if (canExecute) loginViewModel.LoginCommand.Execute();
            // Assert
            Assert.False(loginViewModel.IsAuthenticationCodeRequired);
        }

        [Fact]
        public void ServiceReturnedNeedsAutoCode_ShouldShowAuthCode()
        {
            // Prepare
            var logger = new Mock<ILogger<ViewModels.LoginWithCredentialsViewModel>>().Object;
            var navigationService = new Mock<INavigationService>().Object;
            var authorizationService = new Mock<IAuthorizationService>();
            authorizationService.Setup(x => x.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>(), null))
                .Returns(Task.FromResult(new AuthenticationResult()
                {
                    AuthenticationSuccessful = false,
                    OptionHeader = new Models.Headers.GitHubOptionHeader() { IsRequired = true, Type = Models.Headers.GitHubOptionHeaderType.AuthenticatorApp }
                }));
            var settingsService = new Mock<ISettingsService>().Object;

            LoginWithCredentialsViewModel loginViewModel = new LoginWithCredentialsViewModel(logger, navigationService, authorizationService.Object, settingsService);
            loginViewModel.Username = "A Username";
            loginViewModel.Password = "A Password";

            // Act
            var canExecute = loginViewModel.LoginCommand.CanExecute(null);
            if (canExecute) loginViewModel.LoginCommand.Execute();
            // Assert
            Assert.True(loginViewModel.IsAuthenticationCodeRequired);
        }

    }
}
