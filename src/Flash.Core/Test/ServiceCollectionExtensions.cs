using System;
using Microsoft.Extensions.DependencyInjection;

namespace Flash.Core.Test
{
    public static class ServiceCollectionExtensions
    {
        private static IServiceCollection ReplaceAll<TService>(
            IServiceCollection services,
            Func<IServiceProvider, object> factory, 
            ServiceLifetime lifetime)
        {
            for (var i = 0; i < services.Count; i++)
            {
                if (services[i].ServiceType == typeof(TService))
                {
                    services[i] = new ServiceDescriptor(typeof(TService), factory, lifetime);
                }
            }

            services.Add(ServiceDescriptor.Describe(typeof(TService), factory, lifetime));
            return services;
        }

        public static IServiceCollection ReplaceAsSingleton<TService>(
            this IServiceCollection services,
            Func<IServiceProvider, object> factory)
        {
            return ReplaceAll<TService>(services, factory, ServiceLifetime.Singleton);
        }

        public static IServiceCollection ReplaceAsScoped<TService>(
            this IServiceCollection services,
            Func<IServiceProvider, object> factory)
        {
            return ReplaceAll<TService>(services, factory, ServiceLifetime.Scoped);
        }

        public static IServiceCollection ReplaceAsTransient<TService>(
            this IServiceCollection services,
            Func<IServiceProvider, object> factory)
        {
            return ReplaceAll<TService>(services, factory, ServiceLifetime.Transient);
        }
    }
}
