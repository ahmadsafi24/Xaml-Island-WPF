using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media;

#nullable disable
namespace WinUIXamlIsland
{
    public partial class WinUIControlHost : HwndHost
    {
        private readonly Microsoft.UI.Xaml.Hosting.DesktopWindowXamlSource _xamlSource;
        private readonly Windows.UI.ViewManagement.UISettings uISettings;
        public WinUIControlHost()
        {
            _xamlSource = new Microsoft.UI.Xaml.Hosting.DesktopWindowXamlSource();
            uISettings = new();

            UISettings_ColorValuesChanged(null, null);
            uISettings.ColorValuesChanged += UISettings_ColorValuesChanged;
        }

        private void UISettings_ColorValuesChanged(Windows.UI.ViewManagement.UISettings sender, object args)
        {
            var color = uISettings.GetColorValue(Windows.UI.ViewManagement.UIColorType.Background);
            bgColor = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
        }

        protected override void OnInitialized(EventArgs e)
        {
            _xamlSource.Content = _frame;
            base.OnInitialized(e);

        }

        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            var id = new Microsoft.UI.WindowId((ulong)hwndParent.Handle);

            _xamlSource.Initialize(id);

            // This is the actual child HWND created for the island
            //IntPtr childHwnd = _xamlSource.SiteBridge.SiteWindowHandle;

            _xamlSource.SystemBackdrop = new MicaBackdrop() { Kind = Microsoft.UI.Composition.SystemBackdrops.MicaKind.Base };
            return new HandleRef(null, (nint)_xamlSource.SiteBridge.WindowId.Value);
        }

        public UIElement Content
        {
            get => field;
            set
            {
                field = value;
                _frame.Content = value;
            }
        }

        private readonly Microsoft.UI.Xaml.Controls.Frame _frame = new();

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            _xamlSource.Dispose();
        }

        private System.Drawing.Color bgColor;
        protected const int WM_PAINT = 0x000F; 
        private const int WM_ERASEBKGND = 0x0014;
        protected override nint WndProc(nint hwnd, int msg, nint wParam, nint lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_PAINT:
                    // Handle paint if you want to custom draw into the HWND
                    // (usually you let the hosted control handle this)

                    // Begin painting
                    PAINTSTRUCT ps;
                    nint hdc = BeginPaint(hwnd, out ps);

                    // Fill the client area with black
                    using (Graphics g = Graphics.FromHdc(hdc))
                    {
                        g.Clear(bgColor);
                    }

                    EndPaint(hwnd, ref ps);
                    handled = true; // mark as handled so default proc doesn’t repaint
                    break;
                case WM_ERASEBKGND:
                    // Suppress default background erase to prevent flicker/flash
                    handled = true;
                    return 1; // nonzero = background erased
                default:
                    break;
            }
            return base.WndProc(hwnd, msg, wParam, lParam, ref handled);
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct PAINTSTRUCT
        {
            public IntPtr hdc;
            public bool fErase;
            public RECT rcPaint;
            public bool fRestore;
            public bool fIncUpdate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] rgbReserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr BeginPaint(IntPtr hWnd, out PAINTSTRUCT lpPaint);

        [DllImport("user32.dll")]
        public static extern bool EndPaint(IntPtr hWnd, [In] ref PAINTSTRUCT lpPaint);

    }
}
