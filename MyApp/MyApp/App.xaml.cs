using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using MyApp.Services;
using MyApp.ViewModels;
using MyApp.Views;

namespace MyApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        Services = ConfigureServices();
        MainPage = Services.GetRequiredService<MainPage>();
    }

    public IServiceProvider Services { get; }

    public MainPage MainPage { get; }

    private static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IDateTimeService, DateTimeService>();
        services.AddSingleton<IMessageService, MessageService>();
        services.AddSingleton<MainPageViewModel>();
        services.AddTransient<MainPage>();

        return services.BuildServiceProvider();
    }
}
