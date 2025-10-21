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
    [Route("api/[controller]")]
    [ApiController]
    public class EventoCategoriaController
    {
        private readonly IEventoCategoriaInterface _eventoCategoriaService;

        public EventoCategoriaController(IEventoCategoriaInterface eventoCategoriaService)
        {
            _eventoCategoriaService = eventoCategoriaService;
        }

        // GET: api/EventoCategoria
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var response = await _eventoCategoriaService.ListarEventosCategoria();
            return Ok(response);
        }

        private IActionResult Ok(ResponseModel<List<EventoCategoriaDTO>> response)
        {
            throw new NotImplementedException();
        }

        // GET: api/EventoFaq/buscar?eventoId=1&pergunta=texto
        [HttpGet("buscar")]
        public async Task<IActionResult> Buscar([FromQuery] int categoriaId, [FromQuery] string Nome_Categoria)
        {
            var response = await _eventoCategoriaService.BuscarEventoCategoria(categoriaId, Nome_Categoria);
            return Ok(response);
        }

        private IActionResult Ok(ResponseModel<EventoCategoriaDTO> response)
        {
            throw new NotImplementedException();
        }

        // POST: api/EventoFaq
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] EventoCategoriaDTO eventoCategoria)
        {
            if (eventoCategoria == null)
                return BadRequest(new { Mensagem = "Dados inválidos." });

            var response = await _eventoCategoriaService.CriarEventoCategoria(eventoCategoria);
            return Ok(response);
        }

        private IActionResult BadRequest(object value)
        {
            throw new NotImplementedException();
        }

        // PUT: api/EventoFaq
        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] EventoCategoriaDTO eventoCategoria)
        {
            if (eventoCategoria == null || eventoCategoria.CategoriaId <= 0)
                return BadRequest(new { Mensagem = "ID da Categoria inválido." });

            var response = await _eventoCategoriaService.AtualizarEventoCategoria(eventoCategoria);
            return Ok(response);
        }

        // DELETE: api/EventoFaq/{faqId}
        [HttpDelete("{CategoriaId}")]
        public async Task<IActionResult> Deletar(int categoriaId)
        {
            if (categoriaId <= 0)
                return BadRequest(new { Mensagem = "ID da Categoria inválido." });

            var response = await _eventoCategoriaService.DeletarEventoCategoria(categoriaId);
            return Ok(response);
        }

    }
}
