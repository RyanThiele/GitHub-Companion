using Dynamensions.Infrastructure.Base;
using Dynamensions.Input.Commands;
using GitHubCompanion.Models;
using GitHubCompanion.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GitHubCompanion.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly ILogger _logger;
        private readonly INavigationService _navigationService;
        private readonly ISettingsService _settingsService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IProfileService _profileService;

        #region Constructors

        public HomeViewModel()
        {
            // empty constructor to use with visual designer.
        }

        public HomeViewModel(ILogger<HomeViewModel> logger,
            INavigationService navigationService,
            ISettingsService settingsService,
            IAuthorizationService authorizationService,
            IProfileService profileService)
        {
            _logger = logger;
            _navigationService = navigationService;
            _settingsService = settingsService;
            _authorizationService = authorizationService;
            _profileService = profileService;
        }

        #endregion Constructors

        #region Messages
        #endregion Messages

        #region Properties

        #region IsAuthenticated

        private bool _IsAuthenticated;
        public bool IsAuthenticated
        {
            get { return _IsAuthenticated; }
            private set
            {
                _IsAuthenticated = value;
                OnPropertyChanged();
                LoginWithCredentialsCommand.OnCanExecuteChanged();
                LoginWithTokenCommand.OnCanExecuteChanged();
            }
        }

        #endregion IsAuthenticated

        #endregion Properties

        #region Commands

        #region LoginWithCredentialsCommand

        RelayCommand _LoginWithCredentialsCommand = null;
        public RelayCommand LoginWithCredentialsCommand
        {
            get
            {
                if (_LoginWithCredentialsCommand == null) _LoginWithCredentialsCommand = new RelayCommand(LoginWithCredentialsExecute, CanLoginWithCredentialsExecute);
                return _LoginWithCredentialsCommand;
            }
        }

        protected virtual void LoginWithCredentialsExecute()
        {
            // logic when the command is executed.
        }

        protected virtual bool CanLoginWithCredentialsExecute() { return !IsAuthenticated; }

        #endregion LoginWithCredntials

        #region LoginWithTokenCommand

        RelayCommand _LoginWithTokenCommand = null;
        public RelayCommand LoginWithTokenCommand
        {
            get
            {
                if (_LoginWithTokenCommand == null) _LoginWithTokenCommand = new RelayCommand(LoginWithTokenExecute, CanLoginWithTokenExecute);
                return _LoginWithTokenCommand;
            }
        }

        protected virtual void LoginWithTokenExecute()
        {
            // logic when the command is executed.
        }

        protected virtual bool CanLoginWithTokenExecute() { return !IsAuthenticated; }

        #endregion LoginWithToken

        #region SignOutCommand

        RelayCommand _SignOutCommand = null;
        public RelayCommand SignOutCommand
        {
            get
            {
                if (_SignOutCommand == null) _SignOutCommand = new RelayCommand(SignOutExecute, CanSignOutExecute);
                return _SignOutCommand;
            }
        }

        protected virtual void SignOutExecute()
        {
            // logic when the command is executed.
        }

        protected virtual bool CanSignOutExecute() { return IsAuthenticated; }

        #endregion SignOut

        #endregion Commands

        #region Methods

        public override async Task PrepareViewModelAsync(object parameter = null)
        {
            try
            {
                // check to see if we have a token.
                _logger.LogDebug("Getting token...");
                string token = await _settingsService.GetTokenAsync();
                if (String.IsNullOrWhiteSpace(token))
                {
                    _logger.LogError("No token available.");
                    IsAuthenticated = false;
                    return;
                }

                // token is available, attempt to log in with it.
                _logger.LogInformation("Token available. Attempting to login...");
                Status = "Token available. Logging in...";
                AuthenticationResult tokenResult = await _authorizationService.AuthenticateWithTokenAsync(token);
                if (!tokenResult.AuthenticationSuccessful)
                {
                    // token was invalid
                    _logger.LogInformation("Token was invalid.");
                    _IsAuthenticated = false;

                    // clear token.
                    _logger.LogInformation("Clearing Token...");
                    await _settingsService.ClearTokenAsync();
                    return;
                }

                // if we get to here, everything is valid. 
                IsAuthenticated = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error preparing view model");
            }
        }

        #endregion Methods

    }
}
