using Microsoft.UI.Xaml;
using MyApp;
using MyApp.Services;
using MyApp.Windows.Services;

namespace MyApp.Windows;

public partial class App : Application
{
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        ServiceContainer.Register<INetworkConnectivity>(new WinUiNetworkConnectivity());
        ServiceContainer.Register<IFileAccessService>(new WinUiFileAccessService());

        AppStartup.ConfigureSharedServices();

        var window = new MainWindow();
        WindowHandleProvider.Initialize(window);
        window.Activate();
    }
}
