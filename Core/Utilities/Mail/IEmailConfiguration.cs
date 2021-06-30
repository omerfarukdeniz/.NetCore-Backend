using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Mail
{
    public interface IEmailConfiguration
    {
        string SmtpServer { get; }
        int SmtpPort { get; }
        string SmtpUserName { get; }
        string Password { get; }
    }
}
