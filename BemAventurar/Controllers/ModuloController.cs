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
    public class ModuloController : ControllerBase
    {
        private readonly IModuloInterface _moduloService;
        private readonly IConfiguration _configuration;
        private readonly IModuloInterface _moduloInterface;

        public ModuloController(IModuloInterface moduloService, IConfiguration configuration, IModuloInterface moduloInterface)
        {
            _moduloService = moduloService;
            _configuration = configuration;
            _moduloInterface = moduloInterface;
        }

        [HttpGet("listar-todos")]
        public async Task<IActionResult> ListarTodosModulos()
        {
            var resultado = await _moduloService.ListarModulos();

            if (!resultado.Status)
                return NotFound(resultado);

            return Ok(resultado);
        }

        [HttpGet("pesquisar")]
        public async Task<IActionResult> PesquisarModulos([FromQuery] string? Nome_Modulo)
        {
            var resultado = await _moduloService.PesquisarModulos(Nome_Modulo);

            if (!resultado.Status)
                return NotFound(resultado);

            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> CriarModulo(ModuloDTO moduloDto)
        {
            var modulos = await _moduloInterface.CriarModulo(moduloDto);

            if (modulos.Status == false)
            {
                return BadRequest(modulos);
            }

            return Ok(modulos);

        }

        [HttpPut]
        public async Task<IActionResult> EditarModulo(ModuloDTO moduloDto)
        {
            var modulos = await _moduloInterface.EditarModulo(moduloDto);

            if (modulos.Status == false)
            {
                return BadRequest(modulos);
            }

            return Ok(modulos);
        }

        [HttpDelete("deletar/{moduloId}")]
        public async Task<IActionResult> DeletarModulo(int moduloId)
        {
            var resultado = await _moduloInterface.ExcluirModulo(moduloId);

            if (!resultado.Status)
                return NotFound(resultado);

            return Ok(resultado);
        }
    }
}
