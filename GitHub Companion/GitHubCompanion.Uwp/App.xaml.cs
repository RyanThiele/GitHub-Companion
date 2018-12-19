using GitHubCompanion.Services;
using GitHubCompanion.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace GitHubCompanion.Uwp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            // add all dependencies.
            ServiceCollection serviceDescriptors = new ServiceCollection();

            // logging
            serviceDescriptors.AddLogging(x => x.SetMinimumLevel(LogLevel.Debug).AddDebug());


            // services
            serviceDescriptors.AddSingleton<ISettingsService, LocalServices.SettingsService>();
            serviceDescriptors.AddSingleton<INavigationService, LocalServices.NavigationService>();
            serviceDescriptors.AddSingleton<IAuthorizationService, AuthorizationService>();
            serviceDescriptors.AddSingleton<IProfileService, ProfileService>();

            // ViewModels
            serviceDescriptors.AddTransient<HomeViewModel>();
            serviceDescriptors.AddTransient<LoginWithCredentialsViewModel>();

            // build
            ServiceProvider = serviceDescriptors.BuildServiceProvider();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            // Perform navigation to the home view model.
            INavigationService navigationService = ServiceProvider.GetRequiredService<INavigationService>();
            navigationService.NavigateToAsync<HomeViewModel>();
        }


        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
