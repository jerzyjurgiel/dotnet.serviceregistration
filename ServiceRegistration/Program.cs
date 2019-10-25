using Microsoft.Extensions.DependencyInjection;
using ServiceRegistration.Services;
using ServiceRegistrationLibrary;
using System;
using System.Reflection;

namespace ServiceRegistration
{
    class Program
    {
        static void Main(string[] args)
        {

            var serviceCollection = new ServiceCollection();
            serviceCollection.RegisterCustomServices();
            var serviceProvider = serviceCollection.BuildServiceProvider();


            var service = serviceProvider.GetService<ITestService>();
            service.DoSth();   


            Console.ReadLine();
        }
    }
}
