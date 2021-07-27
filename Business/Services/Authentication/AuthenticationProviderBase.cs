using Business.Adapters.SmsService;
using Business.Constants;
using Business.Services.Authentication.Model;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Toolkit;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Authentication
{
    public abstract class AuthenticationProviderBase : IAuthenticationProvider
    {
        private readonly IMobileLoginRepository _logins;
        private readonly ISmsService _smsService;
        public AuthenticationProviderBase(IMobileLoginRepository logins, ISmsService smsService)
        {
            _logins = logins;
            _smsService = smsService;
        }


        public abstract Task<LoginUserResult> Login(LoginUserCommand command);
        public abstract Task<DArchToken> CreateToken(VerifyOTPCommand command);

        protected virtual async Task<LoginUserResult> PrepareOneTimePassword(AuthenticationProviderType providerType, string cellPhone, string externalUserId)
        {
            var oneTimePassword = await _logins.Query().Where(m => m.Provider == providerType && m.ExternalUserId == externalUserId && m.IsUsed == false).Select(m => m.Code).FirstOrDefaultAsync();

            int mobileCode;
            if (oneTimePassword == default)
            {
                mobileCode = RandomPassword.RandomNumberGenerator();
                try
                {
                    var sendSms = await _smsService.SendAsist($"{DateTime.Now.ToShortTimeString()} itibariyle talep ettiğiniz 24 saat geçerli parolanız : {mobileCode}", cellPhone);
                    _logins.Add(new MobileLogin
                    {
                        Code = mobileCode,
                        IsSend = sendSms,
                        SendDate = DateTime.Now,
                        ExternalUserId = externalUserId,
                        Provider = providerType,
                        IsUsed = true
                    });
                    await _logins.SaveChangesAsync();
                }
                catch
                {
                    return new LoginUserResult { Message = Messages.SmsServiceNotFound, Status = LoginUserResult.LoginStatus.ServiceError };
                }
            }
            else
            {
                mobileCode = oneTimePassword;
            }
            return new LoginUserResult { Message = Messages.SendMobileCode + mobileCode, Status = LoginUserResult.LoginStatus.Ok };
        }


        public async Task<IDataResult<DArchToken>> Verify(VerifyOTPCommand command)
        {
            var externalUserId = command.ExternalUserId;
            var date = DateTime.Now;
            var login = await _logins.GetAsync(m => m.Provider == command.Provider && m.Code == command.Code && m.ExternalUserId == externalUserId && m.SendDate.AddSeconds(100) > date);

            if (login == null)
            {
                return new ErrorDataResult<DArchToken>(Messages.InvalidCode);
            }

            var accessToken = await CreateToken(command);

            if (accessToken.Provider == AuthenticationProviderType.Unknown)
                throw new ArgumentException(Messages.TokenProviderException);

            login.IsUsed = true;
            _logins.Update(login);
            await _logins.SaveChangesAsync();

            return new SuccessDataResult<DArchToken>(accessToken, Messages.SuccessfulLogin);
        }
    }
}
