using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyApp.Commands;

namespace MyApp.Windows;

public class MainWindow : Window
{
    public MainWindow()
    {
        Title = "MyApp";
        Width = 1024;
        Height = 768;

        var commandBar = new CommandBar();
        commandBar.PrimaryCommands.Add(CreateAppBarButton("New", "NewDocument"));
        commandBar.PrimaryCommands.Add(CreateAppBarButton("Open", "OpenDocument"));
        commandBar.PrimaryCommands.Add(CreateAppBarButton("Save", "SaveDocument"));

        Content = commandBar;
    }

    private static AppBarButton CreateAppBarButton(string label, string commandName)
    {
        var button = new AppBarButton { Label = label };
        button.Click += (_, _) => CommandManager.Execute(commandName);
        return button;
    }
}
