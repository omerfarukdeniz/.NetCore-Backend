using Core.Entities.Concrete;
using Core.Utilities.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Authentication.Model
{
    public class VerifyOTPCommand:IRequest<IDataResult<DArchToken>>
    {
        public AuthenticationProviderType Provider { get; set; }
        public string ProviderSubType { get; set; }
        public string ExternalUserId { get; set; }
        public int Code { get; set; }
    }
}
