using GitHubCompanion.Services;
using System.Threading.Tasks;
using Windows.Storage;

namespace GitHubCompanion.Uwp.LocalServices
{
    class SettingsService : ISettingsService
    {
        private const string TOKEN_KEY = "Token";
        private const string PERSONAL_ACCESS_TOKEN_KEY = "Personal Access Token";

        #region Helpers

        private async Task<T> GetSettingAsync<T>(string key)
        {
            var settings = ApplicationData.Current.LocalSettings;
            return (T)(await Task.FromResult(settings.Values[key]));
        }

        private async Task<bool> SetSettingAsync(string key, object value)
        {
            return await Task.FromResult(SetValue(key, value));
        }

        private async Task<bool> ClearSettingAsync(string key)
        {
            var settings = ApplicationData.Current.LocalSettings;
            return await Task.FromResult(settings.Values.Remove(key));
        }

        private bool SetValue(string key, object value)
        {
            var settings = ApplicationData.Current.LocalSettings;
            var existingValue = settings.Values["Token"];
            if (existingValue == null)
            {
                settings.Values.Add(key, value);
            }
            else
            {
                settings.Values[key] = value;
            }
            return true;
        }

        #endregion Helpers

        public Task<bool> ClearTokenAsync()
        {
            return ClearSettingAsync(TOKEN_KEY);
        }

        public Task<string> GetTokenAsync()
        {
            return GetSettingAsync<string>(TOKEN_KEY);
        }

        public Task<bool> SetTokenAsync(string token)
        {
            return SetSettingAsync(TOKEN_KEY, token);
        }

        public Task<string> GetPersonalAccessTokenAsync()
        {
            return GetSettingAsync<string>(PERSONAL_ACCESS_TOKEN_KEY);
        }

        public Task<bool> SetPersonalAccessTokenAsync(string personalAccessToken)
        {
            return SetSettingAsync(PERSONAL_ACCESS_TOKEN_KEY, personalAccessToken);
        }

        public Task<bool> ClearPersonalAccessTokenAsync()
        {
            return ClearSettingAsync(PERSONAL_ACCESS_TOKEN_KEY);
        }
    }
}
