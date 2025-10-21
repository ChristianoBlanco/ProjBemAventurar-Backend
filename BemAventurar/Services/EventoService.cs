using AutoMapper;
using BemAventurar.DTO;
using BemAventurar.Models;
using Dapper;
using System.Data.SqlClient;

namespace BemAventurar.Services
{
    public class EventoService : IEventoInterface
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public EventoService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        private string GetConnection() => _configuration.GetConnectionString("DefaultConnection");

        // Lista todos os eventos
        public async Task<IEnumerable<EventoDTO>> ListarEventos()
        {
            var sql = @"SELECT EventoId, Nome_evento, Desc_evento, Sobre_evento, 
                               Local_evento, Material_evento, Preco_evento, 
                               Data_evento, CategoriaID, CriadoEm
                        FROM Eventos";

            using (var connection = new SqlConnection(GetConnection()))
            {
                return await connection.QueryAsync<EventoDTO>(sql);
            }
        }

        // Busca evento por Id
        public async Task<EventoDTO?> BuscarEventoPorId(int eventoId)
        {
            var sql = @"SELECT EventoId, Nome_evento, Desc_evento, Sobre_evento, 
                               Local_evento, Material_evento, Preco_evento, 
                               Data_evento, CategoriaID, CriadoEm
                        FROM Eventos
                        WHERE EventoId = @EventoId";

            using (var connection = new SqlConnection(GetConnection()))
            {
                return await connection.QueryFirstOrDefaultAsync<EventoDTO>(sql, new { EventoId = eventoId });
            }
        }

        // Cria novo evento
        public async Task<bool> CriarEvento(EventoDTO evento)
        {
            var sql = @"INSERT INTO Eventos 
                        (Nome_evento, Desc_evento, Sobre_evento, Local_evento, 
                         Material_evento, Preco_evento, Data_evento, CategoriaID, CriadoEm)
                        VALUES 
                        (@Nome_evento, @Desc_evento, @Sobre_evento, @Local_evento, 
                         @Material_evento, @Preco_evento, @Data_evento, @CategoriaID, @CriadoEm)";

            using (var connection = new SqlConnection(GetConnection()))
            {
                var rows = await connection.ExecuteAsync(sql, evento);
                return rows > 0;
            }
        }

        // Atualiza evento existente
        public async Task<bool> AtualizarEvento(EventoDTO evento)
        {
            var sql = @"UPDATE Eventos
                        SET Nome_evento = @Nome_evento,
                            Desc_evento = @Desc_evento,
                            Sobre_evento = @Sobre_evento,
                            Local_evento = @Local_evento,
                            Material_evento = @Material_evento,
                            Preco_evento = @Preco_evento,
                            Data_evento = @Data_evento,
                            CategoriaID = @CategoriaID
                        WHERE EventoId = @EventoId";

            using (var connection = new SqlConnection(GetConnection()))
            {
                var rows = await connection.ExecuteAsync(sql, evento);
                return rows > 0;
            }
        }

        // Exclui evento por Id
        public async Task<bool> DeletarEvento(int eventoId)
        {
            var sql = @"DELETE FROM Eventos WHERE EventoId = @EventoId";

            using (var connection = new SqlConnection(GetConnection()))
            {
                var rows = await connection.ExecuteAsync(sql, new { EventoId = eventoId });
                return rows > 0;
            }
        }
    }
}
