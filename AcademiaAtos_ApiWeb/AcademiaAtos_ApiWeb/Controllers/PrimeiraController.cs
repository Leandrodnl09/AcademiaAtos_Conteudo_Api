using AcademiaAtos_ApiWeb.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaAtos_ApiWeb.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PrimeiraController : ControllerBase
    {
        private readonly IJWTAuthenticationManager _authManager;

        public PrimeiraController(IJWTAuthenticationManager authManager)
        {
            this._authManager = authManager;
        }

        [AllowAnonymous]
        [HttpGet("primeiro")]
        public string primeiroEndPoint()
        {
            return "Aula de RestAPI";
        }

        [HttpGet("Nome")]
        public string nome()
        {
            return "Leandro Di Nardo Lazarin";
        }

        [HttpGet("Idade")]
        public int idade()
        {
            return 36;
        }

        [HttpPost("Nome{nome}")]
        public string postNome()
        {
            return "O Nome que recebi foi " + nome();
        }

        [HttpPost("Nome{idade}")]
        public string nomeIdade()
        {
            if (idade() >= 18)
            {
                return nome() + " é maior de idade!";
            }
            else
            {
                return nome() + " é menor de idade!";
            }
        }

        [AllowAnonymous]
        [HttpPost("autenticar")]
        public IActionResult Authenticate([FromBody] Usuario user)
        {
            var token = _authManager.Authenticate(user.Username, user.Password);

            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
