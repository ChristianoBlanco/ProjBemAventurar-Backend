using BemAventurar.DTO;
using BemAventurar.Models;

namespace BemAventurar.Services
{
    public interface IEventoFotoInterface
    {
        Task<ResponseModel<List<EventoFotoDTO>>> ListarEventosFoto();
        Task<ResponseModel<EventoFotoDTO>> BuscarEventoFoto(int eventoId, string nomeFoto);
        Task<ResponseModel<List<EventoFotoDTO>>> CriarEventoFaq(EventoFotoDTO eventoFoto);
        Task<ResponseModel<List<EventoFotoDTO>>> AtualizarEventoFaq(EventoFotoDTO eventoFoto);
        Task<ResponseModel<List<EventoFotoDTO>>> DeletarEventoFaq(int fotoId);
    }
}
