using BemAventurar.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BemAventurar.Services
{
    public interface IEventoInterface
    {
        // Lista todos os eventos
        Task<IEnumerable<EventoDTO>> ListarEventos();

        // Busca evento por Id
        Task<EventoDTO?> BuscarEventoPorId(int eventoId);

        // Cria novo evento
        Task<bool> CriarEvento(EventoDTO evento);

        // Atualiza evento existente
        Task<bool> AtualizarEvento(EventoDTO evento);

        // Exclui evento por Id
        Task<bool> DeletarEvento(int eventoId);
    }
}
