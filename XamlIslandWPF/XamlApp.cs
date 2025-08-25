using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Markup;

namespace XamlIslandWPF
{
    internal partial class XamlApp : Microsoft.UI.Xaml.Application, IXamlMetadataProvider
    {
        public XamlApp()
        {
            _xamlMetaDataProvider = new Microsoft.UI.Xaml.XamlTypeInfo.XamlControlsXamlMetaDataProvider();
            _windowsXamlManager = WindowsXamlManager.InitializeForCurrentThread();
        }

        override protected void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            this.Resources.MergedDictionaries.Add(new Microsoft.UI.Xaml.Controls.XamlControlsResources());
        }

        IXamlType IXamlMetadataProvider.GetXamlType(string fullName)
        {
            var xamlType = _xamlMetaDataProvider.GetXamlType(fullName);
            return xamlType;
        }

        IXamlType IXamlMetadataProvider.GetXamlType(System.Type type)
        {
            var xamlType = _xamlMetaDataProvider.GetXamlType(type);
            return xamlType;
        }

        XmlnsDefinition[] IXamlMetadataProvider.GetXmlnsDefinitions()
        {
            return _xamlMetaDataProvider.GetXmlnsDefinitions();
        }

        private readonly WindowsXamlManager _windowsXamlManager;
        private readonly Microsoft.UI.Xaml.XamlTypeInfo.XamlControlsXamlMetaDataProvider _xamlMetaDataProvider;
    }
}
