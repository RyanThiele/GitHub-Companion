using System.Threading.Tasks;

namespace GitHubCompanion.Services
{
    public interface INavigationService
    {
        Task NavigateToAsync<T>(object parameter = null, bool addtoStack = true) where T : Dynamensions.Infrastructure.Base.ViewModelBase;
        Task NavigateBackAsync(object parameter = null);
    }
}
