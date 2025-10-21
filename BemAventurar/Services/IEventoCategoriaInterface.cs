using BemAventurar.Models;
using BemAventurar.DTO;

namespace BemAventurar.Services
{
    public interface IEventoCategoriaInterface
    {
        Task<ResponseModel<List<EventoCategoriaDTO>>> ListarEventosCategoria();
        Task<ResponseModel<EventoCategoriaDTO>> BuscarEventoCategoria(int categoriaId, string Nome_Categoria);
        Task<ResponseModel<List<EventoCategoriaDTO>>> CriarEventoCategoria(EventoCategoriaDTO eventoCategoria);
        Task<ResponseModel<List<EventoCategoriaDTO>>> AtualizarEventoCategoria(EventoCategoriaDTO eventoCategoria);
        Task<ResponseModel<List<EventoCategoriaDTO>>> DeletarEventoCategoria(int CategoriaId);
    }
}
