using GitHubCompanion.Models;
using GitHubCompanion.Models.User;
using System.Threading.Tasks;

namespace GitHubCompanion.Services
{

    public interface IUserService
    {
        /// <summary>
        /// Gets common characteristics of the user.
        /// </summary>
        /// <returns></returns>
        Task<GitHubResponse<Common>> GetCommonAsync();


    }
}
