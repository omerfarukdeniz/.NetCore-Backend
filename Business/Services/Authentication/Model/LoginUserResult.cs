using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Authentication.Model
{
    public class LoginUserResult
    {
        public LoginStatus Status { get; set; }
        public string Message { get; set; }
        public string[] MobilePhones { get; set; }

        public enum LoginStatus
        {
            UserNotFound,
            WrongCredentials,
            PhoneNumberRequired,
            ServiceError,
            Ok
        }
    }
}
