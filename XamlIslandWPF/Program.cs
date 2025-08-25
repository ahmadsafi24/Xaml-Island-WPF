using System;

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
            // Initialize the application and create the main window
            var app = new App();

            // If you have an App.xaml
            app.InitializeComponent();

            app.Run();
        }

    }

}
