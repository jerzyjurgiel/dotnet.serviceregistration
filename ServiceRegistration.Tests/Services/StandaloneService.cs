using ServiceRegistrationLibrary.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceRegistration.Tests.Services
{
    [TransientService]
    public class StandaloneService
    {
        public void DoSth()
        {
            Console.WriteLine("Standalone service working");
        }
    }
}
