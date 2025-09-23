using BemAventurar.Models;
using BemAventurar.DTO;

namespace BemAventurar.Services
{
    public interface IEventoFaqInterface
    {
        Task<ResponseModel<List<EventoFaqDTO>>> ListarEventosFaq();
        Task<ResponseModel<EventoFaqDTO>> BuscarEventoFaq(int eventoId, string pergunta);
        Task<ResponseModel<List<EventoFaqDTO>>> CriarEventoFaq(EventoFaqDTO eventoFaq);
        Task<ResponseModel<List<EventoFaqDTO>>> AtualizarEventoFaq(EventoFaqDTO eventoFaq);
        Task<ResponseModel<List<EventoFaqDTO>>> DeletarEventoFaq(int faqId);
    }
}


