using BemAventurar.DTO;
using BemAventurar.Models;
using BemAventurar.Services;
using Microsoft.AspNetCore.Mvc;

namespace BemAventurar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoItinerarioController : ControllerBase
    {
        private readonly IEventoItinerarioInterface _eventoItinerarioService;

        public EventoItinerarioController(IEventoItinerarioInterface eventoItinerarioService)
        {
            _eventoItinerarioService = eventoItinerarioService;
        }

        [HttpGet("listar")]
        public async Task<ActionResult<ResponseModel<List<EventoItinerarioDTO>>>> ListarEventosItinerario()
        {
            var response = await _eventoItinerarioService.ListarEventosItinerario();
            return Ok(response);
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<ResponseModel<EventoItinerarioDTO>>> BuscarEventoItinerario([FromQuery] int eventoId, [FromQuery] string nomeEvento)
        {
            if (eventoId <= 0 || string.IsNullOrWhiteSpace(nomeEvento))
            {
                return BadRequest(new ResponseModel<EventoItinerarioDTO>
                {
                    Status = false,
                    Mensagem = "Parâmetros inválidos. Informe EventoID e Itinerario."
                });
            }

            var response = await _eventoItinerarioService.BuscarEventoItinerario(eventoId, nomeEvento);

            if (!response.Status)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost("criar")]
        public async Task<ActionResult<ResponseModel<List<EventoItinerarioDTO>>>> CriarEventoItinerario([FromBody] EventoItinerarioDTO eventoItinerario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _eventoItinerarioService.CriarEventoItinerario(eventoItinerario);
            return Ok(response);
        }

        [HttpPut("atualizar")]
        public async Task<ActionResult<ResponseModel<List<EventoItinerarioDTO>>>> AtualizarEventoItinerario([FromBody] EventoItinerarioDTO eventoItinerario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _eventoItinerarioService.AtualizarEventoItinerario(eventoItinerario);

            if (!response.Status)
                return NotFound(response);

            return Ok(response);
        }

        [HttpDelete("deletar/{itinerarioId}")]
        public async Task<ActionResult<ResponseModel<List<EventoItinerarioDTO>>>> DeletarEventoItinerario(int itinerarioId)
        {
            var response = await _eventoItinerarioService.DeletarEventoItinerario(itinerarioId);

            if (!response.Status)
                return NotFound(response);

            return Ok(response);
        }
    }
}
