using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Authentication
{
    public interface ILoginDataProvider
    {
        Task<LoginDataProviderResult> Verify(string accessToken);
    }

    public class LoginDataProviderResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public ExternalUser ExternalUser { get; set; }
    }

    public class ExternalUser
    {
        public string AgentUserId { get; set; }
        public long CitizenId { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }

        public void CleanRecord()
        {
            if (!string.IsNullOrWhiteSpace(MobilePhone))
            {
                if (!MobilePhone.StartsWith("0"))
                    MobilePhone = "0" + MobilePhone;
                MobilePhone = MobilePhone.Split('-')[0].Trim();
            }
        }
    }
}
