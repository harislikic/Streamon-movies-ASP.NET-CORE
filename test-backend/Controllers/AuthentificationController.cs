using FIT_Api_Examples.Helper;
using haris_edin_rs1.ModelsAutentififkacija;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test_backend.Data;

using test_backend.Models;
using test_backend.ViewModels;
using static FIT_Api_Examples.Helper.AutentifikacijaAutorizacija.MyAuthTokenExtension;


namespace test_backend.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthentificationController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public AuthentificationController(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult<LoginInformacije> Login([FromBody] LoginAddVM x)
        {
            Account acc = _dbContext.Account.FirstOrDefault(y => y.Username != null && y.Username == x.Username && y.Password == x.Password);

            if (acc == null)
            {
                return BadRequest("no user");
            }

            string TokenRandom = TokenGenerator.Generate(20);
            var newToken = new AutentifikacijaToken()
            {
                value = TokenRandom,
                account = acc,
            };
            _dbContext.Add(newToken);
            _dbContext.SaveChanges();

            return new LoginInformacije(newToken);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            AutentifikacijaToken acc = HttpContext.GetAuthToken();
            if (acc == null)
                return Ok("null");

            _dbContext.Remove(acc);
            _dbContext.SaveChanges();
            return Ok("Success");
        }
        [HttpGet]
        public ActionResult<AutentifikacijaToken> Get()
        {
            AutentifikacijaToken acc = HttpContext.GetAuthToken();

            return acc;
        }
    }
}
