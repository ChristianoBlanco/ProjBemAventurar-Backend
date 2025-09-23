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
    public class EventoFotoController : ControllerBase
    {
        private readonly IEventoFotoInterface _eventoFotoService;

        public EventoFotoController(IEventoFotoInterface eventoFotoService)
        {
            _eventoFotoService = eventoFotoService;
        }

        [HttpGet("listar")]
        public async Task<ActionResult<ResponseModel<List<EventoFotoDTO>>>> ListarEventosFoto()
        {
            var response = await _eventoFotoService.ListarEventosFoto();
            return Ok(response);
        }

        /// <summary>
        /// Busca uma foto pelo ID (conforme a interface).
        /// </summary>
        /// <summary>
        /// Busca uma foto pelo EventoID e Nome_foto
        /// </summary>
        [HttpGet("buscar")]
        public async Task<ActionResult<ResponseModel<EventoFotoDTO>>> BuscarEventoFoto([FromQuery] int eventoId, [FromQuery] string nomeFoto)
        {
            if (eventoId <= 0 || string.IsNullOrWhiteSpace(nomeFoto))
            {
                return BadRequest(new ResponseModel<EventoFotoDTO>
                {
                    Status = false,
                    Mensagem = "Parâmetros inválidos. Informe EventoID e Nome_foto."
                });
            }

            var response = await _eventoFotoService.BuscarEventoFoto(eventoId, nomeFoto);

            if (!response.Status)
                return NotFound(response);

            return Ok(response);
        }


        [HttpPost("criar")]
        public async Task<ActionResult<ResponseModel<List<EventoFotoDTO>>>> CriarEventoFoto([FromBody] EventoFotoDTO eventoFoto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _eventoFotoService.CriarEventoFaq(eventoFoto);
            return Ok(response);
        }

        [HttpPut("atualizar")]
        public async Task<ActionResult<ResponseModel<List<EventoFotoDTO>>>> AtualizarEventoFoto([FromBody] EventoFotoDTO eventoFoto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _eventoFotoService.AtualizarEventoFaq(eventoFoto);

            if (!response.Status)
                return NotFound(response);

            return Ok(response);
        }

        [HttpDelete("deletar/{fotoId}")]
        public async Task<ActionResult<ResponseModel<List<EventoFotoDTO>>>> DeletarEventoFoto(int fotoId)
        {
            var response = await _eventoFotoService.DeletarEventoFaq(fotoId);

            if (!response.Status)
                return NotFound(response);

            return Ok(response);
        }
    }
}