using System.ComponentModel;
using System.Runtime.InteropServices;

namespace XamlIslandWPF.Helpers;

public static partial class DwmApi
{
    internal const uint DWMWA_SYSTEMBACKDROP_TYPE = 38;
    internal const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
    internal const int S_OK = 0;

    [LibraryImport("dwmapi.dll", SetLastError = false)]
    internal static partial int DwmExtendFrameIntoClientArea(nint hWnd, in MARGINS pMarInset);

    [LibraryImport("dwmapi.dll", SetLastError = false)]
    internal static partial int DwmSetWindowAttribute(nint hwnd, int attr, ref uint attributeValue, int attributeSize);


    internal static void CheckHResult(int hResult)
    {
        if (hResult != S_OK)
        {
            throw new Win32Exception(hResult);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MARGINS
    {
        /// <summary>Width of the left border that retains its size.</summary>
        public int cxLeftWidth;

        /// <summary>Width of the right border that retains its size.</summary>
        public int cxRightWidth;

        /// <summary>Height of the top border that retains its size.</summary>
        public int cyTopHeight;

        /// <summary>Height of the bottom border that retains its size.</summary>
        public int cyBottomHeight;

        public MARGINS(int allMargins) => cxLeftWidth = cxRightWidth = cyTopHeight = cyBottomHeight = allMargins;
    }

    public enum BackdropType
    {
        Default = 0,
        None = 1,
        Mica = 2,
        Acrylic = 3,
        Tabbed = 4
    }

    /// <summary>
    /// Type of system backdrop to be rendered by DWM
    /// </summary>
    internal enum DWM_SYSTEMBACKDROP_TYPE : uint
    {
        DWMSBT_AUTO = 0,

        /// <summary>
        /// no backdrop
        /// </summary>
        DWMSBT_NONE = 1,

        /// <summary>
        /// Use tinted blurred wallpaper backdrop (Mica)
        /// </summary>
        DWMSBT_MAINWINDOW = 2,

        /// <summary>
        /// Use Acrylic backdrop
        /// </summary>
        DWMSBT_TRANSIENTWINDOW = 3,

        /// <summary>
        /// Use blurred wallpaper backdrop
        /// </summary>
        DWMSBT_TABBEDWINDOW = 4
    }

}
