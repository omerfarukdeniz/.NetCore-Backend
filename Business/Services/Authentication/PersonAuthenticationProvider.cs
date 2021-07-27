using Business.Adapters.SmsService;
using Business.Constants;
using Business.Services.Authentication.Model;
using Core.Entities.Concrete;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Authentication
{
    public class PersonAuthenticationProvider : AuthenticationProviderBase, IAuthenticationProvider
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHelper _tokenHelper;
        public AuthenticationProviderType ProviderType { get; }
        public PersonAuthenticationProvider(AuthenticationProviderType providerType, IUserRepository userRepository, IMobileLoginRepository mobileLogins, ITokenHelper tokenHelper, ISmsService smsService): base(mobileLogins,smsService)
        {
            _userRepository = userRepository;
            ProviderType = providerType;
            _tokenHelper = tokenHelper;
        }


        public override async Task<LoginUserResult> Login(LoginUserCommand command)
        {
            var citizenId = command.AsCitizenId();
            var user = await _userRepository.Query().Where(u => u.CitizenId == citizenId).FirstOrDefaultAsync();

            if (command.IsPhoneValid)
                return await PrepareOneTimePassword(AuthenticationProviderType.Person, user.MobilePhones, user.CitizenId.ToString());
            else
                return new LoginUserResult
                {
                    Message = Messages.TrueButCellPhone,
                    Status = LoginUserResult.LoginStatus.PhoneNumberRequired,
                    MobilePhones = new string[] { user.MobilePhones }
                };
        }

        public override async Task<DArchToken> CreateToken(VerifyOTPCommand command)
        {
            var citizenId = long.Parse(command.ExternalUserId);
            var user = await _userRepository.GetAsync(u => u.CitizenId == citizenId);
            user.AuthenticationProviderType = ProviderType.ToString();

            var claims = _userRepository.GetClaims(user.UserId);
            var accessToken = _tokenHelper.CreateToken<DArchToken>(user, claims);
            accessToken.Provider = ProviderType;
            return accessToken;
        }
    }
}
