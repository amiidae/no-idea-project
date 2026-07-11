using System;
using System.Collections.Generic;

public static class ServiceLocator
{
    private static Dictionary<Type, object> services = new Dictionary<Type, object>();

    public static void RegisterService<T>(T service)
    {
        services[typeof(T)] = service;
    }

    public static T GetService<T>()
    {
        return (T)services[typeof(T)];
    }

    public static bool TryGetService<T>(out T service)
    {
        service = default;

        if (!services.TryGetValue(typeof(T), out object value))
        {
            return false;
        }

        service = (T)value;

        return true;
    }

    public static void Clear()
    {
        services.Clear();
    }
}
