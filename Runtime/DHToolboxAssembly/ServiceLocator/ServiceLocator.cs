using System;
using System.Collections.Generic;
using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.ServiceLocator
{
    public static class ServiceLocator
    {
        private static Dictionary<Type, object> services = new Dictionary<Type, object>();

        public static void AddService<T>(object service) where T : class
        {
            if (services.ContainsKey(typeof(T)))
                throw new Exception($"Service already registered: {typeof(T).Name}");

            services.Add(typeof(T), service);
        }

        public static void AddService(object service)
        {
            var serviceType = service.GetType();
            if (services.ContainsKey(serviceType))
                throw new Exception($"Service already registered: {serviceType}");

            services.Add(serviceType, service);
        }

        public static T GetService<T>() where T : class
        {
            object instance;
            if (services.TryGetValue(typeof(T), out instance))
                return instance as T;

            throw new KeyNotFoundException($"Service type not found: {typeof(T).Name}");
        }

        private static AppConfig.AppConfig config;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        static void Initialize()
        {
            config = Resources.Load<AppConfig.AppConfig>(nameof(AppConfig.AppConfig));
            if (!config)
                config = ScriptableObject.CreateInstance<AppConfig.AppConfig>();

            config.ConfigureAfterAssembliesLoaded();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void ConfigureAfterSceneLoaded()
        {
            config.ConfigureAfterSceneLoaded();
        }
    }
}