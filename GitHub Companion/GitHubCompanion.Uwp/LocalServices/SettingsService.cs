using GitHubCompanion.Services;
using System.Threading.Tasks;
using Windows.Storage;

namespace GitHubCompanion.Uwp.LocalServices
{
    class SettingsService : ISettingsService
    {
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

        public async Task<bool> ClearTokenAsync()
        {
            var settings = ApplicationData.Current.LocalSettings;
            return await Task.FromResult(settings.Values.Remove("Token"));
        }

        public async Task<string> GetTokenAsync()
        {
            var settings = ApplicationData.Current.LocalSettings;
            return (await Task.FromResult(settings.Values["Token"])) as string;
        }

        public async Task<bool> SetTokenAsync(string token)
        {
            return await Task.FromResult(SetValue("Token", token));
        }


    }
}
