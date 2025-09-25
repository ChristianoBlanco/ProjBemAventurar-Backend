using BemAventurar.DTO;
using BemAventurar.Models;

namespace BemAventurar.Services
{
    public interface IEventoVideoInterface
    {
        Task<ResponseModel<List<EventoVideoDTO>>> ListarEventosVideo();
        Task<ResponseModel<EventoVideoDTO>> BuscarEventoVideo(int eventoId, string nome_video);
        Task<ResponseModel<List<EventoVideoDTO>>> CriarEventoVideo(EventoVideoDTO eventoVideo);
        Task<ResponseModel<List<EventoVideoDTO>>> AtualizarEventoVideo(EventoVideoDTO eventoVideo);
        Task<ResponseModel<List<EventoVideoDTO>>> DeletarEventoVideo(int VideoID);
    }
}
