using Microsoft.AspNetCore.Mvc;
using myBURGUERMANIA_API.DTOs.Login;
using myBURGUERMANIA_API.Services;
using myBURGUERMANIA_API.Models;

namespace myBURGUERMANIA_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(User), StatusCodes.Status401Unauthorized)]
        public IActionResult Authenticate([FromBody] LoginDto loginDto)
        {
            var user = _loginService.Authenticate(loginDto);
            if (user == null)
            {
                return Unauthorized(new { mensagem = "Credenciais inválidas." });
            }
            return Ok(user);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public IActionResult Logout([FromBody] LoginDto loginDto)
        {
            _loginService.Logout(loginDto.Email);
            return Ok(new { mensagem = "Logout realizado com sucesso." });
        }
    }
}