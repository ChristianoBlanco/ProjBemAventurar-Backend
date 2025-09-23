using AutoMapper;
using BemAventurar.DTO;
using BemAventurar.Models;
using Dapper;
using System.Data.SqlClient;

namespace BemAventurar.Services
{
    public class EventoFotoService : IEventoFotoInterface
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public EventoFotoService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        // Lista todas as fotos
        public async Task<ResponseModel<List<EventoFotoDTO>>> ListarEventosFoto()
        {
            var response = new ResponseModel<List<EventoFotoDTO>>();

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var fotos = await connection.QueryAsync<EventoFoto>("SELECT * FROM EventoFotos");
                    response.Dados = _mapper.Map<List<EventoFotoDTO>>(fotos.ToList());
                    response.Mensagem = "Fotos listadas com sucesso.";
                    response.Status = true;
                }
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao listar fotos: {ex.Message}";
                response.Status = false;
            }

            return response;
        }

        // Busca foto por Id
        public async Task<ResponseModel<EventoFotoDTO>> BuscarEventoFoto(int eventoId, string nomeFoto)
        {
            var response = new ResponseModel<EventoFotoDTO>();

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var foto = await connection.QueryFirstOrDefaultAsync<EventoFoto>(
                        "SELECT * FROM EventoFotos WHERE EventoID = @EventoID AND Nome_foto = @Nome_foto",
                        new { EventoID = eventoId, Nome_foto = nomeFoto });

                    if (foto == null)
                    {
                        response.Mensagem = "Foto não encontrada.";
                        response.Status = false;
                        return response;
                    }

                    response.Dados = _mapper.Map<EventoFotoDTO>(foto);
                    response.Mensagem = "Foto encontrada com sucesso.";
                    response.Status = true;
                }
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao buscar foto: {ex.Message}";
                response.Status = false;
            }

            return response;
        }


        // Cria nova foto
        public async Task<ResponseModel<List<EventoFotoDTO>>> CriarEventoFaq(EventoFotoDTO eventoFoto)
        {
            var response = new ResponseModel<List<EventoFotoDTO>>();

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var parametros = new
                    {
                        eventoFoto.EventoID,
                        eventoFoto.Nome_foto,
                        eventoFoto.Link_foto,
                        CriadoEm = DateTime.UtcNow
                    };

                    await connection.ExecuteAsync(
                        "INSERT INTO EventoFotos (EventoID, Nome_foto, Link_foto, CriadoEm) " +
                        "VALUES (@EventoID, @Nome_foto, @Link_foto, @CriadoEm)", parametros);

                    var fotos = await connection.QueryAsync<EventoFoto>("SELECT * FROM EventoFotos");
                    response.Dados = _mapper.Map<List<EventoFotoDTO>>(fotos.ToList());
                    response.Mensagem = "Foto criada com sucesso.";
                    response.Status = true;
                }
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao criar foto: {ex.Message}";
                response.Status = false;
            }

            return response;
        }

        // Atualiza foto existente
        public async Task<ResponseModel<List<EventoFotoDTO>>> AtualizarEventoFaq(EventoFotoDTO eventoFoto)
        {
            var response = new ResponseModel<List<EventoFotoDTO>>();

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var fotoExistente = await connection.QueryFirstOrDefaultAsync<EventoFoto>(
                        "SELECT * FROM EventoFotos WHERE FotoId = @FotoId", new { eventoFoto.FotoId });

                    if (fotoExistente == null)
                    {
                        response.Mensagem = "Foto não encontrada.";
                        response.Status = false;
                        return response;
                    }

                    var parametros = new
                    {
                        eventoFoto.EventoID,
                        eventoFoto.Nome_foto,
                        eventoFoto.Link_foto,
                        eventoFoto.FotoId
                    };

                    await connection.ExecuteAsync(
                        "UPDATE EventoFotos SET EventoID = @EventoID, Nome_foto = @Nome_foto, " +
                        "Link_foto = @Link_foto WHERE FotoId = @FotoId", parametros);

                    var fotos = await connection.QueryAsync<EventoFoto>("SELECT * FROM EventoFotos");
                    response.Dados = _mapper.Map<List<EventoFotoDTO>>(fotos.ToList());
                    response.Mensagem = "Foto atualizada com sucesso.";
                    response.Status = true;
                }
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao atualizar foto: {ex.Message}";
                response.Status = false;
            }

            return response;
        }

        // Deleta foto
        public async Task<ResponseModel<List<EventoFotoDTO>>> DeletarEventoFaq(int fotoId)
        {
            var response = new ResponseModel<List<EventoFotoDTO>>();

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var fotoExistente = await connection.QueryFirstOrDefaultAsync<EventoFoto>(
                        "SELECT * FROM EventoFotos WHERE FotoId = @FotoId", new { FotoId = fotoId });

                    if (fotoExistente == null)
                    {
                        response.Mensagem = "Foto não encontrada.";
                        response.Status = false;
                        return response;
                    }

                    await connection.ExecuteAsync("DELETE FROM EventoFotos WHERE FotoId = @FotoId", new { FotoId = fotoId });

                    var fotos = await connection.QueryAsync<EventoFoto>("SELECT * FROM EventoFotos");
                    response.Dados = _mapper.Map<List<EventoFotoDTO>>(fotos.ToList());
                    response.Mensagem = "Foto deletada com sucesso.";
                    response.Status = true;
                }
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao deletar foto: {ex.Message}";
                response.Status = false;
            }

            return response;
        }
    }
}
