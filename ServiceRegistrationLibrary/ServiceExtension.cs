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
                var attribute = type.CustomAttributes.FirstOrDefault(x => x.AttributeType.Name.Contains("ServiceAttribute"));
                if (intrfce != null)
                {
                    RegisterWithInterface(services, intrfce, type, attribute.AttributeType.Name);
                }
                else
                {
                    RegisterWithooutInterface(services, type, attribute.AttributeType.Name);
                }

            }
            return services;

        }

        private static void RegisterWithInterface(ServiceCollection services, Type intrface, Type clss, string attribute)
        {
            switch (attribute)
            {
                case nameof(TransientServiceAttribute):
                    {

                        services.AddTransient(intrface, clss);
                        break;
                    }
                case nameof(SingletonServiceAttribute):
                    {
                        services.AddSingleton(intrface, clss);
                        break;
                    }
                case nameof(ScopedServiceAttribute):
                    {
                        services.AddScoped(intrface, clss);
                        break;
                    }
                default:
                    services.AddTransient(intrface, clss);
                    break;
            }

        }

        private static void RegisterWithooutInterface(ServiceCollection services, Type clss, string attribute)
        {
            switch (attribute)
            {
                case nameof(TransientServiceAttribute):
                    {

                        services.AddTransient(clss);
                        break;
                    }
                case nameof(SingletonServiceAttribute):
                    {
                        services.AddSingleton(clss);
                        break;
                    }
                case nameof(ScopedServiceAttribute):
                    {
                        services.AddScoped(clss);
                        break;
                    }
                default:
                    services.AddTransient(clss);
                    break;
            }
        }


    }
}
