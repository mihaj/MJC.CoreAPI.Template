using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MJC.CoreAPI.Template.WebAPI
{
    public interface IMailService
    {
        void Send(string subject, string message);
    }
}
