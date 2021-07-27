using Core.Utilities.Results;
using System.Threading.Tasks;
using Business.Services.Authentication.Model;

namespace Business.Services.Authentication
{
    public interface IAuthenticationProvider
    {
        Task<LoginUserResult> Login(LoginUserCommand command);
        Task<IDataResult<DArchToken>> Verify(VerifyOTPCommand command);
    }
}
