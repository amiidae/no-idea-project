using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    public static List<T> GetServices<T>()
    {
        // List<T> serviceList = Enumerable.ToList<T>(
        //     services
        //         .Where((keyValue) => typeof(T).IsAssignableFrom(keyValue.Key))
        //         .Select((keyValue) => (T)keyValue.Value)
        // );

        return services.Values.OfType<T>().ToList<T>();
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
