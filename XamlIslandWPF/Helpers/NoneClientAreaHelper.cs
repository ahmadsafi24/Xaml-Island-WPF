using System;
using static XamlIslandWPF.Helpers.DwmApi;

namespace XamlIslandWPF.Helpers;

public partial class NoneClientAreaHelper(nint handle) : IDisposable
{
    private bool disposedValue;

    public void ExtendFrameIntoClientArea()
    {
        MARGINS margins = new(-1);
        int hResult = DwmApi.DwmExtendFrameIntoClientArea(handle, margins);

        CheckHResult(hResult);
    }

    public void ExtendFrameIntoClientArea(MARGINS margins)
    {
        int hResult = DwmApi.DwmExtendFrameIntoClientArea(handle, margins);

        CheckHResult(hResult);
    }

    public void SetBackdropType(BackdropType backdropType)
    {
        DWM_SYSTEMBACKDROP_TYPE dwm_systembackdrop_type = (DWM_SYSTEMBACKDROP_TYPE)backdropType;
        uint backdrop = (uint)dwm_systembackdrop_type;
        int hResult = DwmApi.DwmSetWindowAttribute(handle, (int)DwmApi.DWMWA_SYSTEMBACKDROP_TYPE, ref backdrop, sizeof(uint));

        CheckHResult(hResult);
    }

    public void ToggleImmersiveDarkMode(bool enable)
    {
        uint useImmersiveDarkMode = (uint)(enable ? 1 : 0);
        var result = DwmApi.DwmSetWindowAttribute(handle, DwmApi.DWMWA_USE_IMMERSIVE_DARK_MODE, ref useImmersiveDarkMode, sizeof(int));

        CheckHResult(result);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~WindowApiHelper()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

}
