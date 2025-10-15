using System.Windows;

namespace XamlIslandWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Enable WindowsAppSDK message pre-translation
            //WindowsAppSdkHelper.EnableContentPreTranslateMessageInEventLoop();
        }
    }

}
