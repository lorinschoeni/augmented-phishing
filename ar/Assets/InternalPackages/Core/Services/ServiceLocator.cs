using System;
using System.Collections.Generic;
using UnityEngine;

namespace PhishAR.Core.Services
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<string, IService> _services;

        static ServiceLocator()
        {
            _services = new Dictionary<string, IService>();
        }

        public static void RegisterService<T>(T service) where T : IService
        {
            var serviceName = GetServiceName<T>();

            if (serviceName is null) return;
            if (service == null)
            {
                Debug.Log($"Trying to register null as a {serviceName} service");
                return;
            }

            _services[serviceName] = service;
        }

        public static bool TryGetService<T>(out T service) where T : IService
        {
            service = default;

            var serviceName = GetServiceName<T>();
            if (serviceName is null || !_services.TryGetValue(serviceName, out var registeredService)) return false;

            service = (T) registeredService;
            return true;
        }

        public static void UnregisterService<T>() where T : IService
        {
            var serviceName = GetServiceName<T>();
            if (!IsServiceRegistered<T>())
            {
                Debug.Log($"Trying to unregister service {serviceName} which isn't registered");
                return;
            }

            _services.Remove(serviceName);
        }

        public static bool IsServiceRegistered<T>() where T : IService
        {
            return _services.ContainsKey(GetServiceName<T>());
        }

        public static void UnregisterAllServices()
        {
            _services.Clear();
        }

        private static string GetServiceName<T>()
        {
            var type = typeof(T);
            var serviceName = type.Name;
            // If service has generic types, one per type can be registered.
            Array.ForEach(type.GenericTypeArguments, typeArg => serviceName += typeArg.Name);
            return serviceName;
        }
    }
}
