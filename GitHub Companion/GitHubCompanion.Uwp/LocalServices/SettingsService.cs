using GitHubCompanion.Services;
using System.Threading.Tasks;
using Windows.Storage;

namespace GitHubCompanion.Uwp.LocalServices
{
    class SettingsService : ISettingsService
    {
        public async Task<string> GetTokenAsync()
        {
            var settings = ApplicationData.Current.LocalSettings;
            return (await Task.FromResult(settings.Values["Token"])) as string;
        }
    }
}
