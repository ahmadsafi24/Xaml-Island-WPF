using System.Runtime.InteropServices;

namespace WinUIXaml
{
    internal static partial class DwmApi
    {
        internal const uint DWMWA_SYSTEMBACKDROP_TYPE = 38;
        internal const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
        internal const int S_OK = 0;

        [LibraryImport("dwmapi.dll", SetLastError = false)]
        internal static partial int DwmExtendFrameIntoClientArea(nint hWnd, in MARGINS pMarInset);

        [LibraryImport("dwmapi.dll", SetLastError = false)]
        internal static partial int DwmSetWindowAttribute(nint hwnd, int attr, ref uint attributeValue, int attributeSize);

    }
}