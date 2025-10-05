using System;
using MyApp.Services;
using Windows.Networking.Connectivity;

namespace MyApp.Windows.Services;

public class WinUiNetworkConnectivity : INetworkConnectivity
{
    public bool HasInternetAccess()
    {
        var profile = NetworkInformation.GetInternetConnectionProfile();
        if (profile is null)
        {
            return false;
        }

        return profile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
    }
}
