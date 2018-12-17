using GitHubCompanion.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GitHubCompanion.ViewModels
{
    public class LoginViewModelTest
    {
        [Fact]
        public void AttemptLoginWithEmptyUsername_SholdNotExecute()
        {
            // Prepare
            var logger = new Mock<ILogger<ViewModels.LoginViewModel>>().Object;
            var navigationService = new Mock<INavigationService>().Object;
            var authorizationService = new Mock<IAuthorizationService>().Object;
            var settingsService = new Mock<ISettingsService>().Object;

            LoginViewModel loginViewModel = new ViewModels.LoginViewModel();
            loginViewModel.Username = null;
            loginViewModel.Password = "A Password";

            // Act
            var canExecute = loginViewModel.LoginCommand.CanExecute(null);

            // Assert
            Assert.False(canExecute);
        }

        [Fact]
        public void AttemptLoginWithEmptyPassword_SholdNotExecute()
        {
            // Prepare
            var logger = new Mock<ILogger<ViewModels.LoginViewModel>>().Object;
            var navigationService = new Mock<INavigationService>().Object;
            var authorizationService = new Mock<IAuthorizationService>().Object;
            var settingsService = new Mock<ISettingsService>().Object;

            LoginViewModel loginViewModel = new ViewModels.LoginViewModel();
            loginViewModel.Username = "A Username";
            loginViewModel.Password = null;

            // Act
            var canExecute = loginViewModel.LoginCommand.CanExecute(null);

            // Assert
            Assert.False(canExecute);
        }

        [Fact]
        public void AttemptLoginWithUsernameAndPassword_SholdExecute()
        {
            // Prepare
            var logger = new Mock<ILogger<ViewModels.LoginViewModel>>().Object;
            var navigationService = new Mock<INavigationService>().Object;
            var authorizationService = new Mock<IAuthorizationService>().Object;
            var settingsService = new Mock<ISettingsService>().Object;

            LoginViewModel loginViewModel = new ViewModels.LoginViewModel();
            loginViewModel.Username = "A Username";
            loginViewModel.Password = "A Password";

            // Act
            var canExecute = loginViewModel.LoginCommand.CanExecute(null);

            // Assert
            Assert.True(canExecute);
        }

    }
}
