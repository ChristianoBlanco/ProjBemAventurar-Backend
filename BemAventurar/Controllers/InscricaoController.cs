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
    public class InscricaoController : ControllerBase
    {
        private readonly IInscricaoInterface _inscricaoService;


        public InscricaoController(IInscricaoInterface inscricaoService)
        {
            _inscricaoService = inscricaoService;
        }

        // GET: api/Inscricao
        [HttpGet]
        public async Task<IActionResult> ListarInscricoes()
        {
            var response = await _inscricaoService.ListarInscricoes();
            return Ok(response);
        }


        // GET: api/Inscricao/pesquisar?cliente=...&cpf=...&email=...
        [HttpGet("pesquisar")]
        public async Task<IActionResult> PesquisarInscricoes([FromQuery] string? cliente, [FromQuery] string? cpf, [FromQuery] string? email)
        {
            var response = await _inscricaoService.PesquisarInscricoes(cliente, cpf, email);
            return Ok(response);
        }


        // POST: api/Inscricao
        [HttpPost]
        public async Task<IActionResult> CriarInscricao([FromBody] InscricaoDTO inscricaoDto)
        {
            var response = await _inscricaoService.CriarInscricao(inscricaoDto);
            return Ok(response);
        }


        // PUT: api/Inscricao
        [HttpPut]
        public async Task<IActionResult> EditarInscricao([FromBody] InscricaoDTO inscricaoDto)
        {
            var response = await _inscricaoService.EditarInscricao(inscricaoDto);
            return Ok(response);
        }


        // DELETE: api/Inscricao/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirInscricao(int id)
        {
            var response = await _inscricaoService.ExcluirInscricao(id);
            return Ok(response);
        }
    }
}
    
