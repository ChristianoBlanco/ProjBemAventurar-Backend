using BemAventurar.DTO;
using BemAventurar.Models;
using BemAventurar.Services;
using Microsoft.AspNetCore.Mvc;

namespace BemAventurar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoVideoController : ControllerBase
    {
        private readonly IEventoVideoInterface _eventoVideoService;

        public EventoVideoController(IEventoVideoInterface eventoVideoService)
        {
            _eventoVideoService = eventoVideoService;
        }

        [HttpGet("listar")]
        public async Task<ActionResult<ResponseModel<List<EventoVideoDTO>>>> ListarEventosVideo()
        {
            var response = await _eventoVideoService.ListarEventosVideo();
            return Ok(response);
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<ResponseModel<EventoVideoDTO>>> BuscarEventoVideo(int eventoId, string nome_video)
        {
            var response = await _eventoVideoService.BuscarEventoVideo(eventoId, nome_video);
            return Ok(response);
        }

        [HttpPost("criar")]
        public async Task<ActionResult<ResponseModel<List<EventoVideoDTO>>>> CriarEventoVideo([FromBody] EventoVideoDTO eventoVideo)
        {
            var response = await _eventoVideoService.CriarEventoVideo(eventoVideo);
            return Ok(response);
        }

        [HttpPut("atualizar")]
        public async Task<ActionResult<ResponseModel<List<EventoVideoDTO>>>> AtualizarEventoVideo([FromBody] EventoVideoDTO eventoVideo)
        {
            var response = await _eventoVideoService.AtualizarEventoVideo(eventoVideo);
            return Ok(response);
        }

        [HttpDelete("deletar/{videoId}")]
        public async Task<ActionResult<ResponseModel<List<EventoVideoDTO>>>> DeletarEventoVideo(int videoId)
        {
            var response = await _eventoVideoService.DeletarEventoVideo(videoId);
            return Ok(response);
        }
    }
}
