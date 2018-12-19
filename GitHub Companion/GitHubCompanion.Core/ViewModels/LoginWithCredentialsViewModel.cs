using Dynamensions.Infrastructure.Base;
using Dynamensions.Input.Commands;
using GitHubCompanion.Models;
using GitHubCompanion.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GitHubCompanion.ViewModels
{
    public class LoginWithCredentialsViewModel : ViewModelBase
    {
        private readonly ILogger _logger;
        private readonly INavigationService _navigationService;
        private readonly IAuthorizationService _authorizationService;
        private readonly ISettingsService _settingsService;

        // locals
        private string _clientId = "My Client ID";
        private string _clientSecret = "My Client Secret";

        public enum Modes
        {
            Login,
            AuthenticationCode,
            Failure
        }


        #region Constructors

        public LoginWithCredentialsViewModel()
        {

        }

        public LoginWithCredentialsViewModel(ILogger<LoginWithCredentialsViewModel> logger,
            INavigationService navigationService,
            IAuthorizationService authorizationService,
            ISettingsService settingsService)
        {
            _logger = logger;
            _navigationService = navigationService;
            _authorizationService = authorizationService;
            _settingsService = settingsService;
        }

        #endregion Constructors

        #region Messages
        #endregion Messages

        #region Properties

        private string UseablePassword
        {
            get
            {
                string password = null;
                if (SecurePassword != null && SecurePassword.Length > 0) password = SecurePassword.ConvertToUnsecureString();
                if (!String.IsNullOrWhiteSpace(Password)) password = Password;

                return password;
            }
        }

        #region Username

        private string _Username;
        public string Username
        {
            get { return _Username; }
            set { _Username = value; OnPropertyChanged(); LoginCommand.OnCanExecuteChanged(); }
        }

        #endregion Username

        #region Password

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; OnPropertyChanged(); LoginCommand.OnCanExecuteChanged(); }
        }

        #endregion Password

        #region SecurePassword

        private System.Security.SecureString _SecurePassword;
        public System.Security.SecureString SecurePassword
        {
            get { return _SecurePassword; }
            set { _SecurePassword = value; OnPropertyChanged(); LoginCommand.OnCanExecuteChanged(); }
        }

        #endregion SecurePassword

        #region AuthenticationCode

        private string _AuthenticationCode;
        public string AuthenticationCode
        {
            get { return _AuthenticationCode; }
            set { _AuthenticationCode = value; OnPropertyChanged(); LoginCommand.OnCanExecuteChanged(); }
        }

        #endregion AuthenticationCode

        #region IsAuthenticationCodeRequired

        private bool _IsAuthenticationCodeRequired;
        public bool IsAuthenticationCodeRequired
        {
            get { return _IsAuthenticationCodeRequired; }
            set { _IsAuthenticationCodeRequired = value; OnPropertyChanged(); }
        }

        #endregion IsAuthenticationCodeRequired

        #region IsCreatingToken

        private bool _IsCreatingToken;
        public bool IsCreatingToken
        {
            get { return _IsCreatingToken; }
            private set { _IsCreatingToken = value; OnPropertyChanged(); }
        }

        #endregion IsCreatingToken

        #endregion Properties

        #region Commands

        #region LoginCommand

        RelayCommand _LoginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                if (_LoginCommand == null) _LoginCommand = new RelayCommand(LoginExecute, CanLoginExecute);
                return _LoginCommand;
            }
        }

        protected virtual async void LoginExecute()
        {
            bool isSuccessful = await PerformLoginAsync();
            if (isSuccessful) isSuccessful = await PerformCreateTokenAsync();
            if (isSuccessful) await _navigationService.NavigateToAsync<HomeViewModel>(addtoStack: false);
        }

        protected virtual bool CanLoginExecute()
        {

            string password = null;
            if (SecurePassword != null && SecurePassword.Length > 0) password = SecurePassword.ConvertToUnsecureString();
            if (!String.IsNullOrWhiteSpace(Password)) password = Password;

            bool isUsernameValid = !String.IsNullOrWhiteSpace(Username);
            bool isPasswordValid = !String.IsNullOrWhiteSpace(password);
            bool isAuthenticationCodeValid = !String.IsNullOrWhiteSpace(AuthenticationCode);

            if (IsAuthenticationCodeRequired)
                return isAuthenticationCodeValid;
            else
                return isUsernameValid & isPasswordValid;

        }

        #endregion Login

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

        protected virtual void CancelExecute()
        {

        }

        protected virtual bool CanCancelExecute() { return true; }

        #endregion Cancel

        #endregion Commands

        #region Methods

        private async Task<bool> PerformLoginAsync()
        {
            Status = "Attempting to authenticate...";
            _logger.LogInformation("Attempting to authenticate...");
            AuthenticationResult authenticationResult = await _authorizationService.AuthenticateAsync(Username, UseablePassword, AuthenticationCode);

            // Authentication passed, move to home page.
            if (authenticationResult.AuthenticationSuccessful)
            {
                Status = "Authentication was successful.";
                _logger.LogInformation("Authentication was successful.");
                await Task.Delay(1000);

                _logger.LogInformation("Navigating to Home.");
                return true;
            }

            // Authentication failed. 
            _logger.LogInformation("Authentication Failed.");

            // Does it have two-factor authentication?
            if (authenticationResult.OptionHeader != null && authenticationResult.OptionHeader.IsRequired)
            {
                IsAuthenticationCodeRequired = true;

                _logger.LogInformation($"Authentication requires TFA via {authenticationResult.OptionHeader.Type}.");
                switch (authenticationResult.OptionHeader.Type)
                {
                    case Models.Headers.GitHubOptionHeaderType.Unknown:
                        Status = "Your GitHub account requires a two factor authentication code that is not currently supported.";
                        break;
                    case Models.Headers.GitHubOptionHeaderType.SMS:
                        Status = "Your GitHub account requires a two factor authentication code from your phone.";
                        break;
                    case Models.Headers.GitHubOptionHeaderType.AuthenticatorApp:
                        Status = "Your GitHub account requires a two factor authentication code from your Authenticator App.";
                        break;
                }

                return false;
            }

            // If we get here, all options were exhausted. 
            // Either the user did not enter the correct credentials, 
            // or there is an unknown problem with their account.
            Status = "Could not authenticate with GitHub. Please be sure you have valid credentials, and that there is nothing wrong with your GitHub account.";
            return false;
        }

        private async Task<bool> PerformCreateTokenAsync()
        {
            try
            {
                _logger.LogInformation("Creating token with user and repo permissions.");
                Status = "Attempting to create a token...";

                var response = await _authorizationService.CreateAuthorizationTokenForAppAsync(new AuthorizeParameters()
                {
                    Client_Id = _clientId,
                    Client_Secret = _clientSecret,
                    Note = "GitHub Companion Note",
                    Scopes = new string[] { "user", "repo" }
                }, Username, UseablePassword, AuthenticationCode);

                await _settingsService.SetTokenAsync(response.Response.Hashed_Token);

                return true;
            }
            catch (Exception ex)
            {
                Status = "There was an error creating the token:" + ex.Message;
                _logger.LogError(ex, "Creating Token");
                return false;
            }
        }

        #endregion Methods

    }
}
