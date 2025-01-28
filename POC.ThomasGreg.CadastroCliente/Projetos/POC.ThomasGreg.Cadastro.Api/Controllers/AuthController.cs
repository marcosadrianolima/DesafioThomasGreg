using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuarioLogin loginRequest)
        {
            // Aqui, você pode validar as credenciais com sua base de dados
            if (loginRequest.Usuario == "administrator" && loginRequest.Senha == "senha@123")
            {
                var token = _jwtService.GenerateToken("1", "usuario", "administrator");

                return Ok(new { Token = token });
            }

            return Unauthorized("Credenciais inválidas.");
        }
    }
}
