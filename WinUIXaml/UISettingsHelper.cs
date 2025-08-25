using Microsoft.UI;
using Microsoft.UI.Xaml;
using Windows.UI.ViewManagement;

namespace WinUIXaml
{
    public static class UISettingsHelper
    {
        public static bool GetIsDarkMode()
        {
            return new UISettings().GetColorValue(UIColorType.Background) == Colors.Black;
        }

        public static ElementTheme GetThemeMode()
        {
            if (new UISettings().GetColorValue(UIColorType.Background) == Colors.Black)
            {
                return ElementTheme.Dark;
            }
            else
            {
                return ElementTheme.Light;
            }
        }
    }
}
