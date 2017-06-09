using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using ReceitasDaHora.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(SocialAuthentication))]
namespace ReceitasDaHora.Droid
{
    public class SocialAuthentication : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            try
            {
                var user = await client.LoginAsync(Forms.Context, provider);

                Helpers.Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                Helpers.Settings.UserId = user?.UserId;

                return user;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}