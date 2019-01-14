using GitHubCompanion.Models;
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
        /// Gets the <see cref="GitHubToken"/> used for authenticated calls to the API.
        /// </summary>
        /// <param name="tokenType">The <see cref="TokenTypes"/> type of token.</param>
        /// <returns>A token string, if one exists.</returns>
        Task<GitHubToken> GetTokenAsync(TokenTypes tokenType);

        /// <summary>
        /// Sets the <see cref="GitHubToken"/> used for authenticated calls to the API.
        /// </summary>
        /// <param name="tokenType">The <see cref="TokenTypes"/> type of token.</param>
        /// <returns>True is set was successful; Otherwise, false.</returns>
        /// <param name="token">The token to set.</param>
        Task<bool> SetTokenAsync(GitHubToken gitHubToken);

        /// <summary>
        /// Clears/Resets the <see cref="GitHubToken"/> used for authenticated calls to the API.
        /// </summary>
        /// <param name="tokenType">The <see cref="TokenTypes"/> type of token.</param>
        /// <returns>True is set was successful; Otherwise, false.</returns>
        Task<bool> ClearTokenAsync(TokenTypes tokenType);
    }
}
