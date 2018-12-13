using Dynamensions.Infrastructure.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GitHubCompanion.Uwp.Services.Local
{
    class NavigationService : GitHubCompanion.Services.INavigationService
    {
        private readonly List<Type> _stack = new List<Type>();

        public async Task NavigateBackAsync(object parameter = null)
        {
            // we cannot navigate backwards, so bail.
            if (_stack.Count == 0) return;

            // get the last type
            Type viewModelType = _stack[_stack.Count - 1];
            _stack.RemoveAt(_stack.Count - 1);

            PerformNavigation(viewModelType, parameter, false);
            await Task.Delay(0);
        }

        public async Task NavigateToAsync<T>(object parameter = null, bool addtoStack = true) where T : ViewModelBase
        {
            PerformNavigation(typeof(T), parameter, addtoStack);
            await Task.Delay(0);
        }

        private void PerformNavigation(Type viewModelType, object parameter, bool addtoStack)
        {

            // get the name of the view model from the type.
            string viewModelName = viewModelType.Name;
            // remove the ViewModel from the name.
            string viewName = viewModelName.Replace("Model", "");
            viewName = $"GitHubCompanion.Uwp.Views.{viewName}";
            //viewName = Assembly.CreateQualifiedName(typeof(NavigationService).Assembly.GetName().Name, viewName);
            Type viewType = Type.GetType(viewName);

            // get the root frame.
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                // if navigation fails, throw an exception.
                rootFrame.NavigationFailed += (s, e) =>
                {
                    _stack.RemoveAt(_stack.Count - 1);
                    throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
                };

                // if navigation succeeds, attach the view model, and execute prepare method.
                rootFrame.Navigated += async (s, e) =>
                {
                    var viewModel = (Application.Current as App).ServiceProvider.GetRequiredService(viewModelType) as ViewModelBase;
                    var page = e.Content as Page;
                    page.DataContext = viewModel;
                    await viewModel.PrepareViewModelAsync();
                };

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }


            // perform navigation
            if (parameter != null)
                rootFrame.Navigate(viewType);
            else
                rootFrame.Navigate(viewType);

            if (addtoStack) _stack.Add(viewModelType);
            Window.Current.Activate();

        }

    }
}
