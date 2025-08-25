using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace WinUIXaml
{
    public sealed partial class BlankPage1 : Page
    {
        public BlankPage1()
        {
            InitializeComponent();
            Loaded += BlankPage1_Loaded;
        }

        private void BlankPage1_Loaded(object sender, RoutedEventArgs e)
        {

        }

        int iteration = 0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            iteration++;

            var button = sender as Button;
            button.Content = $"Clicked {iteration} time";
        }
    }
}
