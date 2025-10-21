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
    public class EventoFaqController : ControllerBase
    {
        private readonly IEventoFaqInterface _eventoFaqService;

        public EventoFaqController(IEventoFaqInterface eventoFaqService)
        {
            _eventoFaqService = eventoFaqService;
        }

        // GET: api/EventoFaq
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var response = await _eventoFaqService.ListarEventosFaq();
            return Ok(response);
        }

        // GET: api/EventoFaq/buscar?eventoId=1&pergunta=texto
        [HttpGet("buscar")]
        public async Task<IActionResult> Buscar([FromQuery] int eventoId, [FromQuery] string pergunta)
        {
            var response = await _eventoFaqService.BuscarEventoFaq(eventoId, pergunta);
            return Ok(response);
        }

        // POST: api/EventoFaq
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] EventoFaqDTO eventoFaq)
        {
            if (eventoFaq == null)
                return BadRequest(new { Mensagem = "Dados inválidos." });

            var response = await _eventoFaqService.CriarEventoFaq(eventoFaq);
            return Ok(response);
        }

        // PUT: api/EventoFaq
        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] EventoFaqDTO eventoFaq)
        {
            if (eventoFaq == null || eventoFaq.FaqId <= 0)
                return BadRequest(new { Mensagem = "ID do FAQ inválido." });

            var response = await _eventoFaqService.AtualizarEventoFaq(eventoFaq);
            return Ok(response);
        }

        // DELETE: api/EventoFaq/{faqId}
        [HttpDelete("{faqId}")]
        public async Task<IActionResult> Deletar(int faqId)
        {
            if (faqId <= 0)
                return BadRequest(new { Mensagem = "ID do FAQ inválido." });

            var response = await _eventoFaqService.DeletarEventoFaq(faqId);
            return Ok(response);
        }
    }
}
