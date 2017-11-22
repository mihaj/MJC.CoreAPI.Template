using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MJC.CoreAPI.Template.WebAPI
{
    public class LocalMailService : IMailService
    {
        private string _mailFrom = "my@email.com";
        private string _mailTo = "my2@email.com";

        public void Send(string subject, string message)
        {
            if(String.IsNullOrEmpty(subject))
            {
                throw new Exception("Subject is missing.");
            }

            if (String.IsNullOrEmpty(message))
            {
                throw new Exception("Subject is missing.");
            }

            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with LocalMailService.");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {message}");
        }
    }

}
