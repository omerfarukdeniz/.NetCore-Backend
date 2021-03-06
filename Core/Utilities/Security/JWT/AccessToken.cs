using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class AccessToken : IAccessToken
    {
        public DateTime Expiration { get; set;}
        public string Token { get; set; }
    }
}
