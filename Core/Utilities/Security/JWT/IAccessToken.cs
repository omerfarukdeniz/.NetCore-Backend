using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public interface IAccessToken
    {
        DateTime Expiration { get; set; }
        string Token { get; set; }
    }
}
