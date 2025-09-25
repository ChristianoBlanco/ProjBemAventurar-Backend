using AutoMapper;
using BemAventurar.DTO;
using BemAventurar.Models;
using Dapper;
using System.Data.SqlClient;

namespace BemAventurar.Services
{
    public class EventoItinerarioService : IEventoItinerarioInterface
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public EventoItinerarioService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<EventoItinerarioDTO>>> ListarEventosItinerario()
        {
            var response = new ResponseModel<List<EventoItinerarioDTO>>();
            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                var lista = await connection.QueryAsync<EventoItinerarioDTO>("SELECT * FROM EventoItinerario");
                response.Dados = lista.ToList();
                response.Mensagem = "Lista carregada com sucesso!";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
            }
            return response;
        }

        public async Task<ResponseModel<EventoItinerarioDTO>> BuscarEventoItinerario(int eventoId, string nomeEvento)
        {
            var response = new ResponseModel<EventoItinerarioDTO>();
            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                var query = @"SELECT * FROM EventoItinerario 
                              WHERE EventoId = @EventoId AND NomeEvento = @NomeEvento";
                var result = await connection.QueryFirstOrDefaultAsync<EventoItinerarioDTO>(query, new { EventoId = eventoId, NomeEvento = nomeEvento });

                if (result != null)
                {
                    response.Dados = result;
                    response.Mensagem = "Itinerário encontrado!";
                    response.Status = true;
                }
                else
                {
                    response.Mensagem = "Itinerário não encontrado.";
                    response.Status = false;
                }
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
            }
            return response;
        }

        public async Task<ResponseModel<List<EventoItinerarioDTO>>> CriarEventoItinerario(EventoItinerarioDTO eventoItinerario)
        {
            var response = new ResponseModel<List<EventoItinerarioDTO>>();
            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                var query = @"INSERT INTO EventoItinerario (EventoId, NomeEvento, Data, Local)
                              VALUES (@EventoId, @NomeEvento, @Data, @Local)";

                var result = await connection.ExecuteAsync(query, eventoItinerario);

                if (result > 0)
                {
                    var lista = await connection.QueryAsync<EventoItinerarioDTO>("SELECT * FROM EventoItinerario");
                    response.Dados = lista.ToList();
                    response.Mensagem = "Itinerário criado com sucesso!";
                    response.Status = true;
                }
                else
                {
                    response.Mensagem = "Não foi possível criar o itinerário.";
                    response.Status = false;
                }
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
            }
            return response;
        }

        public async Task<ResponseModel<List<EventoItinerarioDTO>>> AtualizarEventoItinerario(EventoItinerarioDTO eventoItinerario)
        {
            var response = new ResponseModel<List<EventoItinerarioDTO>>();
            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                var query = @"UPDATE EventoItinerario
                              SET NomeEvento = @NomeEvento,
                                  Data = @Data,
                                  Local = @Local
                              WHERE ItinerarioId = @ItinerarioId";

                var result = await connection.ExecuteAsync(query, eventoItinerario);

                if (result > 0)
                {
                    var lista = await connection.QueryAsync<EventoItinerarioDTO>("SELECT * FROM EventoItinerario");
                    response.Dados = lista.ToList();
                    response.Mensagem = "Itinerário atualizado com sucesso!";
                    response.Status = true;
                }
                else
                {
                    response.Mensagem = "Não foi possível atualizar o itinerário.";
                    response.Status = false;
                }
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
            }
            return response;
        }

        public async Task<ResponseModel<List<EventoItinerarioDTO>>> DeletarEventoItinerario(int itinerarioId)
        {
            var response = new ResponseModel<List<EventoItinerarioDTO>>();
            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                var query = @"DELETE FROM EventoItinerario WHERE ItinerarioId = @ItinerarioId";
                var result = await connection.ExecuteAsync(query, new { ItinerarioId = itinerarioId });

                if (result > 0)
                {
                    var lista = await connection.QueryAsync<EventoItinerarioDTO>("SELECT * FROM EventoItinerario");
                    response.Dados = lista.ToList();
                    response.Mensagem = "Itinerário removido com sucesso!";
                    response.Status = true;
                }
                else
                {
                    response.Mensagem = "Não foi possível remover o itinerário.";
                    response.Status = false;
                }
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
            }
            return response;
        }
    }
}
