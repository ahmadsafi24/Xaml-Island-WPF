using System.ComponentModel;
using System.Runtime.InteropServices;

namespace WinUIXaml
{
    /// <summary>
    /// Desktop Window Manager Helper Methods
    /// </summary>
    public static partial class DwmHelper
    {
        public static void ExtendFrameIntoClientArea(nint handle, MARGINS? margins = null)
        {
            MARGINS _margins = margins ?? new MARGINS(-1);
            int hResult = DwmApi.DwmExtendFrameIntoClientArea(handle, _margins);

            CheckHResult(hResult);
        }

        public static void ToggleImmersiveDarkMode(nint handle, bool enable)
        {
            uint useImmersiveDarkMode = (uint)(enable ? 1 : 0);
            var result = DwmApi.DwmSetWindowAttribute(handle, DwmApi.DWMWA_USE_IMMERSIVE_DARK_MODE, ref useImmersiveDarkMode, sizeof(int));

            CheckHResult(result);
        }

        internal static void CheckHResult(int hResult)
        {
            if (hResult != DwmApi.S_OK)
            {
                throw new Win32Exception(hResult);
            }
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

}