using Android.App;
using Android.Content.PM;
using Android.OS;
using MyApp;
using MyApp.Android.Services;
using MyApp.Services;

namespace MyApp.Android;

[Activity(Label = "MyApp", Theme = "@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = true,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
public class MainActivity : Activity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        ServiceContainer.Register<INetworkConnectivity>(new AndroidNetworkConnectivity(this));
        ServiceContainer.Register<IFileAccessService>(new AndroidFileAccessService(this));

        AppStartup.ConfigureSharedServices();
    }
}
