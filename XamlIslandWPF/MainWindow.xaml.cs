using Microsoft.UI.Windowing;
using System.Windows;
using System.Windows.Interop;
using WinUIXaml;
using WinUIXamlIsland;
using XamlIslandWPF.Helpers;

namespace XamlIslandWPF
{
    public partial class MainWindow : Window
    {
        private readonly WinUIControlHost winUIControlHostContent = new();

        public MainWindow()
        {
            this.SourceInitialized += MainWindow_SourceInitialized;
            InitializeComponent();
            ContentRendered += MainWindow_ContentRendered;
        }

        private async void MainWindow_ContentRendered(object? sender, System.EventArgs e)
        {
            await Dispatcher.BeginInvoke(() =>
            {
                winUIControlHostContent.Content = new BlankPage1();
                IslandContentBorder.Child = winUIControlHostContent;
            }, System.Windows.Threading.DispatcherPriority.Background);

        }

        private void MainWindow_SourceInitialized(object? sender, System.EventArgs e)
        {
            WindowInteropHelper windowInteropHelper = new(this);
            var hwnd = windowInteropHelper.Handle;

            AppWindow appWindow = AppWindow.GetFromWindowId(Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd));
            appWindow.TitleBar.PreferredTheme = TitleBarTheme.UseDefaultAppMode;

            NoneClientAreaHelper noneClientAreaHelper = new(hwnd);
            noneClientAreaHelper.SetBackdropType(DwmApi.BackdropType.Mica);
        }
    }
}