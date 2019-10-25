using ServiceRegistrationLibrary.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceRegistration.Services
{
    [SingletonService]

    public class TestService : ITestService
    {
        private readonly IMailService _mailService;

        public TestService(IMailService mailService)
        {
            _mailService = mailService;
        }
        public void DoSth()
        {
            Console.WriteLine("Working");
            _mailService.SendMail();
        }
    }
}
