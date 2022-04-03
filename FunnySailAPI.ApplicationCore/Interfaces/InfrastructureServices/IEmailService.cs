using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Interfaces.InfrastructureServices
{
    public interface IEmailService
    {
        bool SendEmail(string userEmail, string subject, string body);
    }
}
