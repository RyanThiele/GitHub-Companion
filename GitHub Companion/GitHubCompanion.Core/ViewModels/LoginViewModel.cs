using Dynamensions.Infrastructure.Base;
using Dynamensions.Input.Commands;
using GitHubCompanion.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GitHubCompanion.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly ILogger _logger;
        private readonly IAuthorizationService _authorizationService;


        #region Constructors

        public LoginViewModel()
        {

        }

        public LoginViewModel(ILogger<LoginViewModel> logger, IAuthorizationService authorizationService)
        {
            _logger = logger;
            _authorizationService = authorizationService;
        }

        #endregion Constructors

        #region Messages
        #endregion Messages

        #region Properties

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
            await PerformLoginAsync();
        }

        protected virtual bool CanLoginExecute()
        {

            string password = null;
            if (SecurePassword != null && SecurePassword.Length > 0) password = ConvertToUnsecureString(SecurePassword);
            if (!String.IsNullOrWhiteSpace(Password)) password = Password;

            bool isValid = !String.IsNullOrWhiteSpace(Username) && !String.IsNullOrWhiteSpace(password);
            return isValid;
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

        private string ConvertToUnsecureString(System.Security.SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return System.Runtime.InteropServices.Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        private Task PerformLoginAsync()
        {
            string password = null;
            if (SecurePassword != null && SecurePassword.Length > 0) password = ConvertToUnsecureString(SecurePassword);
            if (!String.IsNullOrWhiteSpace(Password)) password = Password;

            return Task.CompletedTask;
        }

        #endregion Methods

    }
}
