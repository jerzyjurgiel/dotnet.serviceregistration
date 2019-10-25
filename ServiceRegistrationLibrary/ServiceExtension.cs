using Microsoft.Extensions.DependencyInjection;
using ServiceRegistrationLibrary.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace ServiceRegistrationLibrary
{
    public static class ServiceExtension
    {

        public static ServiceCollection RegisterCustomServices(this ServiceCollection services)
        {
            var assembly = Assembly.GetCallingAssembly();
            return RegisterCustomServicesFunctionality(services, assembly, "Service");
        }

        public static ServiceCollection RegisterCustomServices(this ServiceCollection services, Assembly assembly)
        {
            return RegisterCustomServicesFunctionality(services, assembly, "Service");
        }


        public static ServiceCollection RegisterCustomServices(this ServiceCollection services, string name)
        {
            var assembly = Assembly.GetCallingAssembly();
            return RegisterCustomServicesFunctionality(services, assembly, name);
        }
        public static ServiceCollection RegisterCustomServices(this ServiceCollection services, Assembly assembly, string name)
        {
            return RegisterCustomServicesFunctionality(services, assembly, name);
        }



        private static ServiceCollection RegisterCustomServicesFunctionality(ServiceCollection services, Assembly assembly, string name)
        {
            var types = assembly.GetTypes().Where(x => x.Name.Contains(name) && x.IsClass);
            foreach (var type in types)
            {
                var intrfce = type.GetInterfaces().FirstOrDefault(x => x.Name.Contains(name));
                if (intrfce == null) continue;
                var attribute = type.CustomAttributes.FirstOrDefault(x => x.AttributeType.Name.Contains("ServiceAttribute"));

                if (attribute == null)
                {
                    services.AddTransient(intrfce, type);
                }
                else if (attribute.AttributeType.Equals(typeof(TransientServiceAttribute)))
                {
                    services.AddTransient(intrfce, type);
                }
                else if (attribute.AttributeType.Equals(typeof(SingletonServiceAttribute)))
                {
                    services.AddSingleton(intrfce, type);
                }
                else if (attribute.AttributeType.Equals(typeof(ScopedServiceAttribute)))
                {
                    services.AddScoped(intrfce, type);
                }
            }
            return services;
        }
    }
}
