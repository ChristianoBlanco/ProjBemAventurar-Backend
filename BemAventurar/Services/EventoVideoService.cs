using AutoMapper;
using BemAventurar.DTO;
using BemAventurar.Models;
using Dapper;
using System.Data.SqlClient;

namespace BemAventurar.Services
{
    public class EventoVideoService : IEventoVideoInterface
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public EventoVideoService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<EventoVideoDTO>>> ListarEventosVideo()
        {
            var response = new ResponseModel<List<EventoVideoDTO>>();
            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                var lista = await connection.QueryAsync<EventoVideoDTO>("SELECT * FROM EventoVideo");
                response.Dados = lista.ToList();
                response.Mensagem = "Lista de vídeos carregada com sucesso!";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
            }
            return response;
        }

        public async Task<ResponseModel<EventoVideoDTO>> BuscarEventoVideo(int eventoId, string nome_video)
        {
            var response = new ResponseModel<EventoVideoDTO>();
            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                var query = @"SELECT * FROM EventoVideo 
                              WHERE EventoID = @EventoID AND nome_video = @Nome_video";
                var result = await connection.QueryFirstOrDefaultAsync<EventoVideoDTO>(query, new { EventoID = eventoId, Nome_video = nome_video });

                if (result != null)
                {
                    response.Dados = result;
                    response.Mensagem = "Vídeo encontrado!";
                    response.Status = true;
                }
                else
                {
                    response.Mensagem = "Vídeo não encontrado.";
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

        public async Task<ResponseModel<List<EventoVideoDTO>>> CriarEventoVideo(EventoVideoDTO eventoVideo)
        {
            var response = new ResponseModel<List<EventoVideoDTO>>();
            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                var query = @"INSERT INTO EventoVideo (EventoID, nome_video, Link_video, CriadoEm)
                              VALUES (@EventoID, @Nome_video, @Link_video, @CriadoEm)";

                eventoVideo.CriadoEm = DateTime.Now;

                var result = await connection.ExecuteAsync(query, eventoVideo);

                if (result > 0)
                {
                    var lista = await connection.QueryAsync<EventoVideoDTO>("SELECT * FROM EventoVideo");
                    response.Dados = lista.ToList();
                    response.Mensagem = "Vídeo criado com sucesso!";
                    response.Status = true;
                }
                else
                {
                    response.Mensagem = "Não foi possível criar o vídeo.";
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

        public async Task<ResponseModel<List<EventoVideoDTO>>> AtualizarEventoVideo(EventoVideoDTO eventoVideo)
        {
            var response = new ResponseModel<List<EventoVideoDTO>>();
            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                var query = @"UPDATE EventoVideo
                              SET nome_video = @Nome_video,
                                  Link_video = @Link_video
                              WHERE VideoID = @VideoID";

                var result = await connection.ExecuteAsync(query, eventoVideo);

                if (result > 0)
                {
                    var lista = await connection.QueryAsync<EventoVideoDTO>("SELECT * FROM EventoVideo");
                    response.Dados = lista.ToList();
                    response.Mensagem = "Vídeo atualizado com sucesso!";
                    response.Status = true;
                }
                else
                {
                    response.Mensagem = "Não foi possível atualizar o vídeo.";
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

        public async Task<ResponseModel<List<EventoVideoDTO>>> DeletarEventoVideo(int videoId)
        {
            var response = new ResponseModel<List<EventoVideoDTO>>();
            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                var query = @"DELETE FROM EventoVideo WHERE VideoID = @VideoID";
                var result = await connection.ExecuteAsync(query, new { VideoID = videoId });

                if (result > 0)
                {
                    var lista = await connection.QueryAsync<EventoVideoDTO>("SELECT * FROM EventoVideo");
                    response.Dados = lista.ToList();
                    response.Mensagem = "Vídeo removido com sucesso!";
                    response.Status = true;
                }
                else
                {
                    response.Mensagem = "Não foi possível remover o vídeo.";
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
