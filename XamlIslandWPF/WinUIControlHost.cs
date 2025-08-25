using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml.Media;
using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media;
using WinUIXaml;

namespace XamlIslandWPF
{
    public partial class WinUIControlHost : HwndHost
    {
        private readonly Microsoft.UI.Xaml.Hosting.DesktopWindowXamlSource _xamlSource;

        public WinUIControlHost()
        {
            _xamlSource = new Microsoft.UI.Xaml.Hosting.DesktopWindowXamlSource();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
        }

        protected override void OnInitialized(EventArgs e)
        {
            InitIslandSampleCode();
            base.OnInitialized(e);
        }

        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            var id = new Microsoft.UI.WindowId((ulong)hwndParent.Handle);

            var windowId = id;//XamlRoot.ContentIsland.Environment.AppWindowId;
            var hwnd = Microsoft.UI.Win32Interop.GetWindowFromWindowId(windowId);
            var appWindow = AppWindow.GetFromWindowId(windowId);

            DwmHelper.ExtendFrameIntoClientArea(hwnd, new MARGINS(-1));

            DwmHelper.ToggleImmersiveDarkMode(hwnd, UISettingsHelper.GetIsDarkMode());

            appWindow.TitleBar.ExtendsContentIntoTitleBar = true;
            appWindow.TitleBar.PreferredTheme = TitleBarTheme.UseDefaultAppMode;

            appWindow.TitleBar.ButtonBackgroundColor = Microsoft.UI.Colors.Transparent;
            appWindow.TitleBar.ButtonInactiveBackgroundColor = Microsoft.UI.Colors.Transparent;

            _xamlSource.Initialize(id);

            //to hide white flicker on resize
            _xamlSource.SiteBridge.Hide();

            _xamlSource.SystemBackdrop = new MicaBackdrop() { Kind = Microsoft.UI.Composition.SystemBackdrops.MicaKind.Base };
            return new HandleRef(null, (nint)_xamlSource.SiteBridge.WindowId.Value);
        }

        private void InitIslandSampleCode()
        {
            _xamlSource.Content = new BlankPage1();
        }

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            _xamlSource.Dispose();
        }
    }
}
