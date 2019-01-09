using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GitHubCompanion.Uwp.Conveters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // parameter
            bool isInverted = false;
            if (parameter != null) Boolean.TryParse(parameter.ToString(), out isInverted);

            // value
            bool isVisible = false;
            if (value != null) Boolean.TryParse(value.ToString(), out isVisible);

            // inversion
            if (isInverted) isVisible = !isVisible;

            // result
            if (isVisible)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
