using Dynamensions.Infrastructure.Base;
using GitHubCompanion.Services;
using Microsoft.Extensions.Logging;

namespace GitHubCompanion.ViewModels
{
    public class TokenViewModel : ViewModelBase
    {
        private readonly ILogger _logger;
        private readonly ISettingsService _settingsService;

        #region Constructors

        public TokenViewModel()
        {
            // Empty constructor for design time binding.
        }

        public TokenViewModel(ILogger<TokenViewModel> logger, ISettingsService settingsService)
        {
            _logger = logger;
            _settingsService = settingsService;
        }

        #endregion Constructors

        #region Messages
        #endregion Messages

        #region Properties
        #endregion Properties

        #region Commands
        #endregion Commands

        #region Methods
        #endregion Methods

    }
}
