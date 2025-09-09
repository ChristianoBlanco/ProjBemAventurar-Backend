using BemAventurar.DTO;
using BemAventurar.Models;
using BemAventurar.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace BemAventurar.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioService;
        private readonly IConfiguration _configuration;
        private readonly IUsuarioInterface _usuarioInterface;

        public UsuarioController(IUsuarioInterface usuarioService, IConfiguration configuration, IUsuarioInterface usuarioInterface)
        {
            _usuarioService = usuarioService;
            _configuration = configuration;
            _usuarioInterface = usuarioInterface;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO login)
        {
            var usuario = await _usuarioService.AutenticarUsuarioAsync(login.Email, login.Senha);
            if (usuario == null)
                return Unauthorized("Credenciais inválidas");

            var token = GerarToken(usuario);
            return Ok(new { token });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(new { message = "Logout efetuado com sucesso" });
        }

        private string GerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.ClienteId.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome ?? ""),
                new Claim(ClaimTypes.Email, usuario.Email ?? "")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet("listar-todos")]
        public async Task<IActionResult> ListarTodosUsuarios()
        {
            var resultado = await _usuarioService.ListarUsuarios();

            if (!resultado.Status)
                return NotFound(resultado);

            return Ok(resultado);
        }


        [HttpGet("pesquisar")]
        public async Task<IActionResult> PesquisarUsuarios([FromQuery] string? nome, [FromQuery] string? email, [FromQuery] int? clienteId)
        {
            var resultado = await _usuarioService.PesquisarUsuarios(nome, email, clienteId);

            if (!resultado.Status)
                return NotFound(resultado);

            return Ok(resultado);
        }


        [HttpPost]
        public async Task<IActionResult> CriarUsuario(UsuarioDTO usuarioDto)
        {
            var usuarios = await _usuarioInterface.CriarUsuario(usuarioDto);

            if (usuarios.Status == false)
            {
                return BadRequest(usuarios);
            }

            return Ok(usuarios);

        }

        [HttpPut]
        public async Task<IActionResult> EditarUsuario(UsuarioDTO usuarioDto)
        {
            var usuarios = await _usuarioInterface.EditarUsuario(usuarioDto);

            if (usuarios.Status == false)
            {
                return BadRequest(usuarios);
            }

            return Ok(usuarios);
        }

        [HttpDelete("deletar/{clienteId}")]
        public async Task<IActionResult> DeletarUsuario(int clienteId)
        {
            var resultado = await _usuarioService.ExcluirUsuario(clienteId);

            if (!resultado.Status)
                return NotFound(resultado);

            return Ok(resultado);
        }



    }
}
