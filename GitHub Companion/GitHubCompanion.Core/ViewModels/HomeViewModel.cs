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
        // services
        private readonly ILogger _logger;
        private readonly INavigationService _navigationService;
        private readonly ISettingsService _settingsService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IProfileService _profileService;

        // locals
        private GitHubToken _token;

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
                SignOutCommand.OnCanExecuteChanged();
            }
        }

        #endregion IsAuthenticated

        #region Profile

        private Profile _Profile;
        public Profile Profile
        {
            get { return _Profile; }
            set { _Profile = value; OnPropertyChanged(); }
        }

        #endregion Profile

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

        protected async virtual void LoginWithCredentialsExecute()
        {
            await _navigationService.NavigateToAsync<LoginWithCredentialsViewModel>(addtoStack: false);
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

        protected virtual async void SignOutExecute()
        {
            await _settingsService.ClearTokenAsync(TokenTypes.AuthorizationToken);
            await _settingsService.ClearTokenAsync(TokenTypes.PersonalAccessToken);
            await PrepareViewModelAsync();
        }

        protected virtual bool CanSignOutExecute() { return IsAuthenticated; }

        #endregion SignOut

        #endregion Commands

        #region Methods

        public override async Task PrepareViewModelAsync(object parameter = null)
        {
            try
            {
                if (Globals.Token == null)
                {
                    // are we authenticated?
                    IsAuthenticated = await PerformAuthenticateWithTokenAsync();
                    if (!IsAuthenticated) IsAuthenticated = await PerformAuthenticateWithPersonalAccessTokenAsync();
                    if (!IsAuthenticated) return;

                    // saver the token.
                    Globals.Token = new GlobalProperty<GitHubToken>(_token);
                }

                if (Globals.Profile == null || Globals.Profile.LastUpdated < DateTime.Now.AddDays(1))
                {
                    Profile profile = await GetSelfProfileInformationAsync(_token.Token);
                    Profile = profile;
                    Globals.Profile = new GlobalProperty<Profile>(profile);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error preparing view model");
            }
        }

        private async Task<bool> PerformAuthenticateWithTokenAsync()
        {
            // check to see if we have a token.
            _logger.LogDebug("Getting token...");
            _token = await _settingsService.GetTokenAsync(TokenTypes.AuthorizationToken);
            if (_token == null || !_token.IsValid)
            {
                _logger.LogError("No token available.");
                IsAuthenticated = false;
                return false;
            }

            // token is available, attempt to log in with it.
            _logger.LogInformation("Token available. Attempting to login...");
            Status = "Token available. Logging in...";
            AuthenticationResult tokenResult = await _authorizationService.AuthenticateWithTokenAsync(_token.Token);
            if (!tokenResult.AuthenticationSuccessful)
            {
                // token was invalid
                _logger.LogInformation("Token was invalid.");
                _IsAuthenticated = false;

                // clear token.
                _logger.LogInformation("Clearing Token...");
                await _settingsService.ClearTokenAsync(TokenTypes.AuthorizationToken);
                return false;
            }

            return true;
        }

        private async Task<bool> PerformAuthenticateWithPersonalAccessTokenAsync()
        {
            // check to see if we have a token.
            _logger.LogDebug("Getting personal access token...");
            _token = await _settingsService.GetTokenAsync(TokenTypes.PersonalAccessToken);
            if (_token == null || !_token.IsValid)
            {
                _logger.LogError("No personal token available.");
                IsAuthenticated = false;
                return false;
            }

            // token is available, attempt to log in with it.
            _logger.LogInformation("Personal access token available. Attempting to login...");
            Status = "Token available. Logging in...";
            AuthenticationResult tokenResult = await _authorizationService.AuthenticateWithPersonalAccessTokenAsync(_token.Token);
            if (!tokenResult.AuthenticationSuccessful)
            {
                // token was invalid
                _logger.LogInformation("Personal access token was invalid.");
                _IsAuthenticated = false;

                // clear token.
                _logger.LogInformation("Clearing Personal access token...");
                await _settingsService.ClearTokenAsync(TokenTypes.PersonalAccessToken);
                return false;
            }

            return true;
        }

        private async Task<Profile> GetSelfProfileInformationAsync(string token)
        {
            _logger.LogInformation("Getting profile information...");
            GitHubResponse<Profile> response = await _profileService.GetSelfProfileAsync(token);

            response.UpdateGlobals();

            if (response == null)
                return null;
            else
                return response.Response;
        }

        #endregion Methods

    }
}
