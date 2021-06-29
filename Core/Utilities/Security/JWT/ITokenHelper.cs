using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        TAccessToken CreateToken<TAccessToken>(User user, IEnumerable<OperationClaim> operationClaims) where TAccessToken : IAccessToken, new();
    }
}
