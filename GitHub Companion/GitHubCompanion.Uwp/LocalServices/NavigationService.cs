using Dynamensions.Infrastructure.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GitHubCompanion.Uwp.LocalServices
{
    class NavigationService : GitHubCompanion.Services.INavigationService
    {
        private readonly List<Type> _stack = new List<Type>();
        private Type _currentViewModelType = null;
        private Frame _rootFrame = new Frame();

        public NavigationService()
        {
            _rootFrame.Navigated += _rootFrame_Navigated;
            _rootFrame.Navigating += _rootFrame_Navigating;
            _rootFrame.NavigationFailed += _rootFrame_NavigationFailed;
            Window.Current.Content = _rootFrame;
        }

        private void _rootFrame_NavigationStopped(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            if (e.Content == null) return;
            Page view = e.Content as Page;
            ViewModelBase viewModel = view.DataContext as ViewModelBase;
            viewModel.Dispose();
        }

        private void _rootFrame_NavigationFailed(object sender, Windows.UI.Xaml.Navigation.NavigationFailedEventArgs e)
        {
            _stack.RemoveAt(_stack.Count - 1);
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void _rootFrame_Navigating(object sender, Windows.UI.Xaml.Navigation.NavigatingCancelEventArgs e)
        {
            if (_rootFrame.Content != null)
            {
                Page view = _rootFrame.Content as Page;
                ViewModelBase viewModel = view.DataContext as ViewModelBase;
                viewModel.Dispose();
                _rootFrame.Content = null;
            }
        }

        private void _rootFrame_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            var viewModel = (Application.Current as App).ServiceProvider.GetRequiredService(_currentViewModelType) as ViewModelBase;
            var page = e.Content as Page;
            page.Loaded += (s, ev) => { viewModel.PrepareViewModelAsync(); };
            page.DataContext = viewModel;
        }

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
            _currentViewModelType = viewModelType;
            // get the name of the view model from the type.
            string viewModelName = viewModelType.Name;
            // remove the ViewModel from the name.
            string viewName = viewModelName.Replace("Model", "");
            viewName = $"GitHubCompanion.Uwp.Views.{viewName}";
            //viewName = Assembly.CreateQualifiedName(typeof(NavigationService).Assembly.GetName().Name, viewName);
            Type viewType = Type.GetType(viewName);

            // perform navigation
            if (parameter != null)
                _rootFrame.Navigate(viewType);
            else
                _rootFrame.Navigate(viewType);

            if (addtoStack) _stack.Add(viewModelType);
            Window.Current.Activate();
        }

    }
}
