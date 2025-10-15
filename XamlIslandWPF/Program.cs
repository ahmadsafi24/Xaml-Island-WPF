using System;

#nullable disable
namespace XamlIslandWPF
{
    public class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            WinRT.ComWrappersSupport.InitializeComWrappers();

            // Initialize the application and create the main window
            var app = new App();

            // Island-support: This is necessary for Xaml controls, 
            // resources, and metatdata to work correctly.
            xamlApp = new XamlApp();

            // If you have an App.xaml
            app.InitializeComponent();

            app.Run();

            xamlApp.Shutdown();
        }

        internal static XamlApp xamlApp;
    }

}
