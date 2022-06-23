using System.Linq;
using System.Text.Json.Serialization;
using haris_edin_rs1.ModelsAutentififkacija;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using test_backend.Data;
using test_backend.Models;

namespace FIT_Api_Examples.Helper.AutentifikacijaAutorizacija
{
    public static class MyAuthTokenExtension
    {
        public class LoginInformacije
        {
            public LoginInformacije(AutentifikacijaToken autentifikacijaToken)
            {
                this.autentifikacijaToken = autentifikacijaToken;
            }

            [JsonIgnore]
            public Account account => autentifikacijaToken?.account;
            public AutentifikacijaToken autentifikacijaToken { get; set; }

            public bool isLogiran => account != null;

            public bool isPermisijaKorisnik => isLogiran && (account.user != null  );
          
        }


        public static LoginInformacije GetLoginInfo(this HttpContext httpContext)
        {
            var token = httpContext.GetAuthToken();

            return new LoginInformacije(token);
        }

        public static AutentifikacijaToken GetAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.GetMyAuthToken();
            AppDbContext db = httpContext.RequestServices.GetService<AppDbContext>();

            AutentifikacijaToken korisnickiNalog = db.AutentifikacijaToken
               .Include(s => s.account)
               .SingleOrDefault(x => token != null && x.value == token);

            return korisnickiNalog;
        }


        public static string GetMyAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["autentifikacija-token"];
            return token;
        }

    }
}
