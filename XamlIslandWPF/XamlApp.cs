using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.XamlTypeInfo;
using System;
using System.Diagnostics;
using System.Threading;

namespace XamlIslandWPF
{
    internal partial class XamlApp : Microsoft.UI.Xaml.Application, IXamlMetadataProvider
    {
        public readonly DispatcherQueueController controller;
        public void Shutdown()
        {
            // Island-support: Shut down the DispatcherQueue and all the WindowsAppSDK UI objects on the thread.
            controller.ShutdownQueue();
        }

        public XamlApp()
        {
            // Island-support: This is required to use the WindowsAppSDK UI stack on the thread.
            controller = DispatcherQueueController.CreateOnCurrentThread();

            var context = new DispatcherQueueSynchronizationContext(controller.DispatcherQueue);
            SynchronizationContext.SetSynchronizationContext(context);

            // Island-support: This allows the WindowsAppSDK UI stack to process messages before the WinForms message loop.
            WindowsAppSdkHelper.EnableContentPreTranslateMessageInEventLoop();

            _xamlMetaDataProvider = new Microsoft.UI.Xaml.XamlTypeInfo.XamlControlsXamlMetaDataProvider();
            _windowsXamlManager = WindowsXamlManager.InitializeForCurrentThread();

        }

        override protected void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            Current.UnhandledException += Current_UnhandledException;
            Current.DebugSettings.XamlResourceReferenceFailed += DebugSettings_XamlResourceReferenceFailed;
            Current.DebugSettings.BindingFailed += DebugSettings_BindingFailed;

            Resources.MergedDictionaries.Add(new Microsoft.UI.Xaml.Controls.XamlControlsResources());
        }

        private void DebugSettings_BindingFailed(object sender, Microsoft.UI.Xaml.BindingFailedEventArgs e)
        {
            Debug.WriteLine("DebugSettings_BindingFailed: " + e.Message);
        }

        private void DebugSettings_XamlResourceReferenceFailed(Microsoft.UI.Xaml.DebugSettings sender, Microsoft.UI.Xaml.XamlResourceReferenceFailedEventArgs args)
        {
            Debug.WriteLine("DebugSettings_XamlResourceReferenceFailed: " + args.Message);
        }

        private void Current_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            Debug.WriteLine("Current_UnhandledException: " + e.Message);
        }

        IXamlType IXamlMetadataProvider.GetXamlType(string fullName)
        {
            return _xamlMetaDataProvider.GetXamlType(fullName);
        }

        IXamlType IXamlMetadataProvider.GetXamlType(Type type)
        {
            return _xamlMetaDataProvider.GetXamlType(type);
        }

        XmlnsDefinition[] IXamlMetadataProvider.GetXmlnsDefinitions()
        {
            return _xamlMetaDataProvider.GetXmlnsDefinitions();
        }

        private readonly WindowsXamlManager _windowsXamlManager;
        private readonly XamlControlsXamlMetaDataProvider _xamlMetaDataProvider;
    }
}
