using System.Threading.Tasks;

namespace GitHubCompanion.Services
{
    /* 
     * For security reasons, the application WILL NOT store a password.
     * For all authentication request, a token will be used. 
     * If the token does not allow the request, then the request will be unavailable.
     * The user, however can update the token's permissions in GitHub; which will allow the request.
     */

    public interface ISettingsService
    {
        /// <summary>
        /// Get the token used for authenticated calls to the API.
        /// </summary>
        /// <returns>A token string, if one exists.</returns>
        Task<string> GetTokenAsync();
    }
}
