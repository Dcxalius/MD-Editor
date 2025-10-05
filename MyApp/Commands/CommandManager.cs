using System;
using System.Collections.Concurrent;

namespace MyApp.Commands;

public static class CommandManager
{
    private static readonly ConcurrentDictionary<string, Action> _commands = new(StringComparer.OrdinalIgnoreCase);

    public static void Register(string name, Action handler)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Command name must be provided.", nameof(name));
        }

        if (handler is null)
        {
            throw new ArgumentNullException(nameof(handler));
        }

        _commands[name] = handler;
    }

    public static bool Execute(string name)
    {
        if (name is null)
        {
            return false;
        }

        if (_commands.TryGetValue(name, out var handler))
        {
            handler();
            return true;
        }

        return false;
    }
}
