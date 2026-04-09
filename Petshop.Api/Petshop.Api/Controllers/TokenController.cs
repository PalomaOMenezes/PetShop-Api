using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Petshop.Api.DTO;
using Petshop.Api.Interface;

namespace Petshop.Api.Controllers
{
    [ApiController]
    [Route("login")]
    public class TokenController : Controller
    {

        private ITokenService _services;

        public TokenController(ITokenService services)
        {
            _services = services;
        }

        [HttpPost]
        public async Task<object> Login(LoginDTO login)
        {
            if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Senha))
            {
                return "Email ou senha em branco";
            }

            var result = await _services.EfetuarLogin(login.Email, login.Senha);

            if (result == null) 
            { 
                return HttpStatusCode.NotFound.ToString();
            }

            return result;


        }



    }
}
