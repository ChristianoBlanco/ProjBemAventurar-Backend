using BemAventurar.DTO;
using BemAventurar.Models;

namespace BemAventurar.Services
{
    public interface IEventoItinerarioInterface
    {
        Task<ResponseModel<List<EventoItinerarioDTO>>> ListarEventosItinerario();
        Task<ResponseModel<EventoItinerarioDTO>> BuscarEventoItinerario(int eventoId, string nomeEvento);
        Task<ResponseModel<List<EventoItinerarioDTO>>> CriarEventoItinerario(EventoItinerarioDTO eventoItinerario);
        Task<ResponseModel<List<EventoItinerarioDTO>>> AtualizarEventoItinerario(EventoItinerarioDTO eventoItinerario);
        Task<ResponseModel<List<EventoItinerarioDTO>>> DeletarEventoItinerario(int itinerarioId);
    }
}
