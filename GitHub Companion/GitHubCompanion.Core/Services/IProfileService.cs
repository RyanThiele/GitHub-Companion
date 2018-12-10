using GitHubCompanion.Models;
using System.Threading.Tasks;

namespace GitHubCompanion.Services
{
    public interface IProfileService
    {
        Task<Profile> GetUserProfileAsync(string username);
    }
}
