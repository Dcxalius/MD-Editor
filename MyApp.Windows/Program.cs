using System;
using Microsoft.UI.Xaml;

namespace MyApp.Windows;

public static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        Application.Start(_ => new App());
    }
}
