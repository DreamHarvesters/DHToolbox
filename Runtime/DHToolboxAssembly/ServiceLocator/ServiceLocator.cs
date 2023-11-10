using System;
using System.Collections.Generic;

namespace DHToolbox.Runtime.DHToolboxAssembly.ServiceLocator
{
    public static class ServiceLocator
    {
        private static Dictionary<Type, object> services = new Dictionary<Type, object>();

        public static void SetService<T>(object service) where T : class
        {
            if (!services.ContainsKey(typeof(T)))
                throw new Exception($"Service not exist: {typeof(T).Name}");

            services[typeof(T)] = service;
        }

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
    }
}