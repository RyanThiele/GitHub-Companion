using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace GitHubCompanion.Uwp.Controls
{
    public sealed partial class GitHubMark : UserControl
    {
        public GitHubMark()
        {
            this.InitializeComponent();
        }

        private void GitHubMarkControl_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            LoadingStoryboard.Begin();
        }
    }
}
