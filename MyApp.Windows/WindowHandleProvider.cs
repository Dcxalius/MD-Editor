using System;
using Microsoft.UI.Xaml;

namespace MyApp.Windows;

public static class WindowHandleProvider
{
    private static IntPtr _windowHandle;

    public static void Initialize(Window window)
    {
        _windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
    }

    public static IntPtr GetHandle()
    {
        if (_windowHandle == IntPtr.Zero)
        {
            throw new InvalidOperationException("Window handle has not been initialized.");
        }

        return _windowHandle;
    }
}
