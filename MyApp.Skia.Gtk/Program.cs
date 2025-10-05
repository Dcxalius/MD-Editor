using System;
using Gtk;
using MyApp;
using MyApp.Commands;
using MyApp.Services;

namespace MyApp.Skia.Gtk;

public static class Program
{
    public static void Main(string[] args)
    {
        Application.Init();

        ServiceContainer.Register<INetworkConnectivity>(new GtkNetworkStub());
        ServiceContainer.Register<IFileAccessService>(new GtkFileAccessStub());
        AppStartup.ConfigureSharedServices();

        var window = new Window("MyApp")
        {
            DefaultWidth = 1024,
            DefaultHeight = 768,
            Resizable = true
        };

        var menuBar = BuildMenuBar();
        var vbox = new VBox(false, 2);
        vbox.PackStart(menuBar, false, false, 0);
        window.Add(vbox);

        window.DeleteEvent += (_, _) => CommandManager.Execute("Exit");

        window.ShowAll();
        Application.Run();
    }

    private static MenuBar BuildMenuBar()
    {
        var menuBar = new MenuBar();

        var fileMenu = new Menu();
        fileMenu.Append(CreateMenuItem("New", "NewDocument"));
        fileMenu.Append(CreateMenuItem("Open", "OpenDocument"));
        fileMenu.Append(CreateMenuItem("Save", "SaveDocument"));
        fileMenu.Append(new SeparatorMenuItem());
        fileMenu.Append(CreateMenuItem("Exit", "Exit"));

        var root = new MenuItem("File") { Submenu = fileMenu };
        menuBar.Append(root);

        return menuBar;
    }

    private static MenuItem CreateMenuItem(string text, string commandName)
    {
        var item = new MenuItem(text);
        item.Activated += (_, _) => CommandManager.Execute(commandName);
        return item;
    }

    private class GtkNetworkStub : INetworkConnectivity
    {
        public bool HasInternetAccess() => true;
    }

    private class GtkFileAccessStub : IFileAccessService
    {
        public System.Threading.Tasks.Task<string?> PickOpenFileAsync(string[] mimeTypes) =>
            System.Threading.Tasks.Task.FromResult<string?>(null);

        public System.Threading.Tasks.Task SaveTextAsync(string filePath, string contents) =>
            System.Threading.Tasks.Task.CompletedTask;
    }
}
