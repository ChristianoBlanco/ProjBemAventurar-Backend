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
    public class EventoController : ControllerBase
    {
        private readonly IEventoInterface _eventoService;

        public EventoController(IEventoInterface eventoService)
        {
            _eventoService = eventoService;
        }

        // GET: api/Evento
        [HttpGet]
        public async Task<IActionResult> ListarEventos()
        {
            try
            {
                var eventos = await _eventoService.ListarEventos();
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao listar eventos: {ex.Message}");
            }
        }

        // GET: api/Evento/5
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarEventoPorId(int id)
        {
            try
            {
                var evento = await _eventoService.BuscarEventoPorId(id);
                if (evento == null)
                    return NotFound($"Evento com ID {id} não encontrado.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar evento: {ex.Message}");
            }
        }

        // POST: api/Evento
        [HttpPost]
        public async Task<IActionResult> CriarEvento([FromBody] EventoDTO evento)
        {
            try
            {
                evento.CriadoEm = DateTime.Now;

                var criado = await _eventoService.CriarEvento(evento);
                if (!criado)
                    return BadRequest("Não foi possível criar o evento.");

                return Ok("Evento criado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar evento: {ex.Message}");
            }
        }

        // PUT: api/Evento/5
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarEvento(int id, [FromBody] EventoDTO evento)
        {
            try
            {
                var existente = await _eventoService.BuscarEventoPorId(id);
                if (existente == null)
                    return NotFound($"Evento com ID {id} não encontrado.");

                evento.EventoId = id; // garante que atualize o registro correto

                var atualizado = await _eventoService.AtualizarEvento(evento);
                if (!atualizado)
                    return BadRequest("Não foi possível atualizar o evento.");

                return Ok("Evento atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar evento: {ex.Message}");
            }
        }

        // DELETE: api/Evento/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarEvento(int id)
        {
            try
            {
                var existente = await _eventoService.BuscarEventoPorId(id);
                if (existente == null)
                    return NotFound($"Evento com ID {id} não encontrado.");

                var deletado = await _eventoService.DeletarEvento(id);
                if (!deletado)
                    return BadRequest("Não foi possível excluir o evento.");

                return Ok("Evento excluído com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir evento: {ex.Message}");
            }
        }
    }
}
