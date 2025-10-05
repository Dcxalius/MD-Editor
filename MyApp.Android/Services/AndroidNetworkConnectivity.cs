using Android.Content;
using Android.Net;
using Android.Net.Wifi;
using MyApp.Services;

namespace MyApp.Android.Services;

public class AndroidNetworkConnectivity : INetworkConnectivity
{
    private readonly ConnectivityManager _connectivityManager;

    public AndroidNetworkConnectivity(Context context)
    {
        _connectivityManager = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService)!;
    }

    public bool HasInternetAccess()
    {
        if (OperatingSystem.IsAndroidVersionAtLeast(23))
        {
            var capabilities = _connectivityManager.GetNetworkCapabilities(_connectivityManager.ActiveNetwork);
            return capabilities?.HasCapability(NetCapability.Internet) == true &&
                   capabilities?.HasCapability(NetCapability.Validated) == true;
        }

        var activeNetworkInfo = _connectivityManager.ActiveNetworkInfo;
        return activeNetworkInfo?.IsConnected == true;
    }
}
