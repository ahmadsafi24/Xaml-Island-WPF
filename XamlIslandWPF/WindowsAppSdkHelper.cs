using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace XamlIslandWPF;

internal static class WindowsAppSdkHelper
{
    /// <summary>
    /// Call this once your WPF app starts using the WindowsAppSDK UI stack (WinUI 3, etc.).
    /// Hooks into the WPF message pump via ComponentDispatcher.
    /// </summary>
    public static void EnableContentPreTranslateMessageInEventLoop()
    {
        ComponentDispatcher.ThreadFilterMessage += OnThreadFilterMessage;
    }

    private static void OnThreadFilterMessage(ref MSG msg, ref bool handled)
    {
        if (ContentPreTranslateMessage(ref msg))
        {
            handled = true;
        }
    }

    [DllImport("Microsoft.UI.Windowing.Core.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool ContentPreTranslateMessage(ref MSG message);
}