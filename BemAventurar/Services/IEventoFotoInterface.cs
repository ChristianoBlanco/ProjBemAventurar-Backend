using BemAventurar.DTO;
using BemAventurar.Models;

namespace BemAventurar.Services
{
    public interface IEventoFotoInterface
    {
        Task<ResponseModel<List<EventoFotoDTO>>> ListarEventosFoto();
        Task<ResponseModel<EventoFotoDTO>> BuscarEventoFoto(int eventoId, string nomeFoto);
        Task<ResponseModel<List<EventoFotoDTO>>> CriarEventoFoto(EventoFotoDTO eventoFoto);
        Task<ResponseModel<List<EventoFotoDTO>>> AtualizarEventoFoto(EventoFotoDTO eventoFoto);
        Task<ResponseModel<List<EventoFotoDTO>>> DeletarEventoFoto(int fotoId);
    }
}
