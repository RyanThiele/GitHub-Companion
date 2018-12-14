using GitHubCompanion.Models;
using System.Threading.Tasks;

namespace GitHubCompanion.Services
{
    public interface IProfileService
    {
        /// <summary>
        /// Gets the owners (self) profile.
        /// </summary>
        /// <param name="token">A valid token.</param>
        /// <returns>A <see cref="GitHubResponse{Profile}"/> object containing the response from GitHub API.</returns>
        Task<GitHubResponse<Profile>> GetSelfProfileAsync(string token);

        /// <summary>
        /// Gets a user's profile.
        /// </summary>
        /// <param name="username">The username of the profile to view.</param>
        /// <returns>A <see cref="GitHubResponse{Profile}"/> object containing the response from GitHub API.</returns>
        Task<GitHubResponse<Profile>> GetUserProfileAsync(string username);
    }
}
