using GitHubCompanion.Services;
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
            serviceDescriptors.AddLogging(x => x.SetMinimumLevel(LogLevel.Debug));

            // services
            serviceDescriptors.AddSingleton<INavigationService, Services.Local.NavigationService>();
            serviceDescriptors.AddSingleton<IAuthorizationService, AuthorizationService>();

            // ViewModels
            serviceDescriptors.AddTransient<ViewModels.LoginViewModel>();

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







            //Frame rootFrame = Window.Current.Content as Frame;

            //// Do not repeat app initialization when the Window already has content,
            //// just ensure that the window is active
            //if (rootFrame == null)
            //{
            //    // Create a Frame to act as the navigation context and navigate to the first page
            //    rootFrame = new Frame();

            //    rootFrame.NavigationFailed += OnNavigationFailed;

            //    if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
            //    {
            //        //TODO: Load state from previously suspended application
            //    }

            //    // Place the frame in the current Window
            //    Window.Current.Content = rootFrame;
            //}

            //if (e.PrelaunchActivated == false)
            //{
            //    if (rootFrame.Content == null)
            //    {
            //        // When the navigation stack isn't restored navigate to the first page,
            //        // configuring the new page by passing required information as a navigation
            //        // parameter
            //        rootFrame.Navigate(typeof(MainPage), e.Arguments);
            //    }
            //    // Ensure the current window is active
            //    Window.Current.Activate();
            //}

            INavigationService navigationService = ServiceProvider.GetRequiredService<INavigationService>();
            navigationService.NavigateToAsync<ViewModels.LoginViewModel>();
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
