using ServiceRegistration.Tests.Services;
using ServiceRegistrationLibrary.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceRegistration.Tests.Services
{
    [TransientService]
    public class MailService : IMailService
    {
        public void SendMail()
        {
            Console.WriteLine("Mail Sent");
        }
    }
}
