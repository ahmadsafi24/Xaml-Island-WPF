using Microsoft.UI.Windowing;
using System.Windows;
using System.Windows.Interop;
using WinUIXaml;
using WinUIXamlIsland;

namespace XamlIslandWPF
{
    public partial class MainWindow : Window
    {
        private readonly WinUIControlHost winUIControlHostContent = new();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowInteropHelper windowInteropHelper = new(this);
            var hwnd = windowInteropHelper.Handle;

            AppWindow appWindow = AppWindow.GetFromWindowId(Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd));
            //appWindow.TitleBar.ExtendsContentIntoTitleBar = true;
            appWindow.TitleBar.PreferredTheme = TitleBarTheme.UseDefaultAppMode;
            //appWindow.TitleBar.ButtonBackgroundColor = Microsoft.UI.Colors.Transparent;
            //appWindow.TitleBar.ButtonInactiveBackgroundColor = Microsoft.UI.Colors.Transparent;

            IslandContentBorder.Child = winUIControlHostContent;

            winUIControlHostContent.Content = new BlankPage1();


        }

        //protected override void OnRender(DrawingContext drawingContext)
        //{
        //    // Use the control's current render size
        //    Rect rect = new Rect(0, 0, RenderSize.Width, RenderSize.Height);

        //    // Fill 
        //    drawingContext.DrawRectangle(Brushes.LightBlue, null, rect);
        //    base.OnRender(drawingContext);

        //}
    }
}