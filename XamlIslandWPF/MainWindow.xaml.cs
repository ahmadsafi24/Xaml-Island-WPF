using Microsoft.UI.Dispatching;
using System;
using System.Windows;

namespace XamlIslandWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DispatcherQueueController _controller;
        private WinUIControlHost? _winUIControl;

        public MainWindow()
        {
            _controller = DispatcherQueueController.CreateOnCurrentThread();

            // Island-support: This is necessary for Xaml controls, 
            // resources, and metatdata to work correctly.
            var winUIApp = new XamlApp();

            InitializeComponent();
            Loaded += MainWindow_Loaded;

            #region Net9 Theme
            // Type is for evaluation purposes only and
            // is subject to change or removal in future updates.
            // Suppress this diagnostic to proceed.
#pragma warning disable WPF0001
            this.ThemeMode = ThemeMode.System;
#pragma warning restore WPF0001

            #endregion

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _winUIControl = new WinUIControlHost();
            ControlHostElement.Child = _winUIControl;
        }

    }
}