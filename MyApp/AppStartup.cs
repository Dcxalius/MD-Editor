using System;
using MyApp.Commands;
using MyApp.Services;

namespace MyApp;

public static class AppStartup
{
    public static void ConfigureSharedServices()
    {
        CommandManager.Register("NewDocument", () => Console.WriteLine("New document requested"));
        CommandManager.Register("OpenDocument", () => Console.WriteLine("Open document requested"));
        CommandManager.Register("SaveDocument", () => Console.WriteLine("Save document requested"));
        CommandManager.Register("Exit", () => Console.WriteLine("Exit requested"));
    }

    public static bool EnsureConnectivity()
    {
        var connectivity = ServiceContainer.Resolve<INetworkConnectivity>();
        return connectivity.HasInternetAccess();
    }
}
