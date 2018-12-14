using Dynamensions.Infrastructure.Base;
using GitHubCompanion.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GitHubCompanion.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly ILogger _logger;
        private readonly ISettingsService settingsService;
        private readonly IProfileService _profileService;
        private readonly INavigationService _navigationService;

        #region Constructors

        public HomeViewModel()
        {
            // empty constructor to use with visual designer.
        }

        public HomeViewModel(ILogger logger, IProfileService profileService)
        {
            _logger = logger;
            _profileService = profileService;
        }

        #endregion Constructors

        #region Messages
        #endregion Messages

        #region Properties
        #endregion Properties

        #region Commands
        #endregion Commands

        #region Methods

        public override async Task PrepareViewModelAsync(object parameter = null)
        {
            try
            {
                // check to see if we have a token.
                _logger.LogDebug("Getting token...");
                string token = await settingsService.GetTokenAsync();
                if (String.IsNullOrWhiteSpace(token))
                {
                    _logger.LogError("No token available.");
                    _logger.LogInformation("Navigating to Login");
                    await _navigationService.NavigateToAsync<LoginViewModel>();
                    return;
                }

                _logger.LogInformation("Token available.");


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error preparing view model");
            }
        }

        #endregion Methods

    }
}
