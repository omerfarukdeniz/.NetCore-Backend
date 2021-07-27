using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using wsKPSPublic;

namespace Business.Adapters.PersonService
{
    public class PersonServiceManager : IPersonService
    {
        public static async Task<bool> Verify(long tCKimlikNo, string ad, string soyad, int dogumYili)
        {
            var locale = new CultureInfo("tr-TR", false);
            var svc = new KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);
            {
                var cmd = await svc.TCKimlikNoDogrulaAsync(
                    tCKimlikNo,
                    ad.ToUpper(locale),
                    soyad.ToUpper(locale),
                    dogumYili);

                return cmd.Body.TCKimlikNoDogrulaResult;
            }
        }
        public Task<bool> VerifyCid(long tCKimlikNo, string ad, string soyad, int dogumYili)
        {
            throw new NotImplementedException();
        }


    }
}
