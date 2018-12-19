using Dynamensions.Infrastructure.Base;
using Dynamensions.Input.Commands;
using GitHubCompanion.Models;
using GitHubCompanion.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GitHubCompanion.ViewModels
{
    public class LoginWithTokenViewModel : ViewModelBase
    {
        private readonly ILogger _logger;
        private readonly ISettingsService _settingsService;
        private readonly INavigationService _navigationService;
        private readonly IAuthorizationService _authorizationService;

        #region Constructors

        public LoginWithTokenViewModel()
        {
            // Empty constructor for design time binding.
        }

        public LoginWithTokenViewModel(ILogger<LoginWithTokenViewModel> logger, ISettingsService settingsService, INavigationService navigationService, IAuthorizationService authorizationService)
        {
            _logger = logger;
            _settingsService = settingsService;
            _navigationService = navigationService;
            _authorizationService = authorizationService;
        }

        #endregion Constructors

        #region Messages
        #endregion Messages

        #region Properties

        #region Token

        private string _Token;
        public string Token
        {
            get { return _Token; }
            set { _Token = value; OnPropertyChanged(); AuthenticateCommand.OnCanExecuteChanged(); }
        }

        #endregion Token

        #endregion Properties

        #region Commands

        #region AuthenticateCommand

        RelayCommand _AuthenticateCommand = null;
        public RelayCommand AuthenticateCommand
        {
            get
            {
                if (_AuthenticateCommand == null) _AuthenticateCommand = new RelayCommand(AuthenticateExecute, CanAuthenticateExecute);
                return _AuthenticateCommand;
            }
        }

        protected virtual async void AuthenticateExecute()
        {
            await AuthenticateWithTokenAsync();
        }

        protected virtual bool CanAuthenticateExecute() { return !String.IsNullOrEmpty(Token); }

        #endregion Authenticate

        #region CancelCommand

        RelayCommand _CancelCommand = null;
        public RelayCommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null) _CancelCommand = new RelayCommand(CancelExecute, CanCancelExecute);
                return _CancelCommand;
            }
        }

        protected virtual async void CancelExecute()
        {
            await _navigationService.NavigateToAsync<HomeViewModel>(addtoStack: false);
        }

        protected virtual bool CanCancelExecute() { return true; }

        #endregion Cancel

        #endregion Commands

        #region Methods

        private async Task AuthenticateWithTokenAsync()
        {
            bool isValidToken = false;
            try
            {
                Status = "Attempting to authenticate with token...";
                _logger.LogInformation("Authenticating with token...");
                AuthenticationResult result = await _authorizationService.AuthenticateWithTokenAsync(Token);
                isValidToken = result.AuthenticationSuccessful;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error authenticating with token.");
                Status = "There was an error authenticating with the token: " + ex.Message;
            }

            if (isValidToken) await _navigationService.NavigateToAsync<HomeViewModel>(addtoStack: false);
        }

        #endregion Methods

    }
}
