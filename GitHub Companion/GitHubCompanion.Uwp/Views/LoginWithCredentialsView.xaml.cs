using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GitHubCompanion.Uwp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginWithCredentialsView : Page
    {

        public LoginWithCredentialsView()
        {
            this.InitializeComponent();
        }

        private void TextBox_IsEnabledChanged(object sender, Windows.UI.Xaml.DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (sender != null && textBox.Visibility == Windows.UI.Xaml.Visibility.Visible) textBox.Focus(Windows.UI.Xaml.FocusState.Keyboard);
        }
    }
}
