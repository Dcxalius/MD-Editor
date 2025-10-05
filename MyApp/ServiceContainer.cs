using System;
using System.Collections.Concurrent;

namespace MyApp;

public static class ServiceContainer
{
    private static readonly ConcurrentDictionary<Type, object> _registrations = new();

    public static void Register<TService>(TService implementation) where TService : class
    {
        if (implementation is null)
        {
            throw new ArgumentNullException(nameof(implementation));
        }

        _registrations[typeof(TService)] = implementation;
    }

    public static TService Resolve<TService>() where TService : class
    {
        if (_registrations.TryGetValue(typeof(TService), out var implementation))
        {
            return (TService)implementation;
        }

        throw new InvalidOperationException($"Service {typeof(TService).FullName} is not registered.");
    }
}
