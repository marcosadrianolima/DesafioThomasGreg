using Microsoft.AspNetCore.Mvc;
using POC.ThomasGreg.Cadastro.Api.Configuracao;

namespace POC.ThomasGreg.Cadastro.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("Login")]
        public TokenResult Login([FromBody] UsuarioLogin loginRequest)
        {
            if (loginRequest.Usuario == "administrador" && loginRequest.Senha == "senha@123")
            {
                var token = _jwtService.GenerateToken("1", "usuario", "administrator");

                return new TokenResult() { Token = token };
            }

            return new TokenResult();
        }
    }
}
