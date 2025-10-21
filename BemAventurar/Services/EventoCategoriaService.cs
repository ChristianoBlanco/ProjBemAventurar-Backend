using AutoMapper;
using BemAventurar.DTO;
using BemAventurar.Models;
using Dapper;
using System.Data.SqlClient;

namespace BemAventurar.Services
{
    public class EventoCategoriaService : IEventoCategoriaInterface
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public EventoCategoriaService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<EventoCategoriaDTO>>> ListarEventosCategoria()
        {
            var response = new ResponseModel<List<EventoCategoriaDTO>>();

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var categorias = await connection.QueryAsync<EventoCategoria>("SELECT * FROM Evento_categorias");
            response.Dados = _mapper.Map<List<EventoCategoriaDTO>>(categorias);
            response.Mensagem = "Lista de Categorias carregada com sucesso.";
            response.Status = true;

            return response;
        }

        public async Task<ResponseModel<EventoCategoriaDTO>> BuscarEventoCategoria(int categoriaId, string Nome_Categoria)
        {
            var response = new ResponseModel<EventoCategoriaDTO>();

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var categoria = await connection.QueryFirstOrDefaultAsync<EventoCategoria>(
                "SELECT * FROM Eventos_Categorias WHERE CategoriaID = @CategoriaID AND Nome_Categoria = @Nome_Categoria",
                new { CategoriaID = categoriaId, Nome_Categoria = Nome_Categoria });

            if (categoria == null)
            {
                response.Mensagem = "Categoria não encontrada.";
                response.Status = true;
                return response;
            }

            response.Dados = _mapper.Map<EventoCategoriaDTO>(categoria);
            response.Mensagem = "Categoria encontrada com sucesso.";
            response.Status = true;

            return response;
        }

        public async Task<ResponseModel<List<EventoCategoriaDTO>>> CriarEventoCategoria(EventoCategoriaDTO eventoCategoria)
        {
            var response = new ResponseModel<List<EventoCategoriaDTO>>();

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var parametros = new
            {
                eventoCategoria.Nome_Categoria,
                CriadoEm = DateTime.UtcNow
            };

            await connection.ExecuteAsync(
                "INSERT INTO Evento_Categotias (EventoID, Nome_Categoria, CriadoEm) " +
                "VALUES (@EventoID, @Nome_Categoria, @CriadoEm)", parametros);

            var categorias = await connection.QueryAsync<EventoCategoria>("SELECT * FROM Evento_Categorias");
            response.Dados = _mapper.Map<List<EventoCategoriaDTO>>(categorias);
            response.Mensagem = "Categoria criada com sucesso.";
            response.Status = true;

            return response;
        }

        public async Task<ResponseModel<List<EventoCategoriaDTO>>> AtualizarEventoCategoria(EventoCategoriaDTO eventoCategoria)
        {
            var response = new ResponseModel<List<EventoCategoriaDTO>>();

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var categoriaExistente = await connection.QueryFirstOrDefaultAsync<EventoCategoria>(
                "SELECT * FROM Evento_Categorias WHERE CategoriaId = @CategoriaId", new { eventoCategoria.CategoriaId });

            if (categoriaExistente == null)
            {
                response.Mensagem = "Categoria não encontrada.";
                response.Status = true;
                return response;
            }

            var parametros = new
            {
                eventoCategoria.Nome_Categoria,
                eventoCategoria.CategoriaId
            };

            await connection.ExecuteAsync(
                "UPDATE Evento_Categorias SET CategoriaID = @CategoriaID, Nome_Categoria = @Nome_Categoria, " +
                "WHERE CategoriaId = @CategoriaId", parametros);

            var categorias = await connection.QueryAsync<EventoCategoria>("SELECT * FROM Evento_Categorias");
            response.Dados = _mapper.Map<List<EventoCategoriaDTO>>(categorias);
            response.Mensagem = "Categoria atualizada com sucesso.";
            response.Status = true;

            return response;
        }

        public async Task<ResponseModel<List<EventoCategoriaDTO>>> DeletarEventoCategoria(int CategoriaId)
        {
            var response = new ResponseModel<List<EventoCategoriaDTO>>();

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var CategoriaExistente = await connection.QueryFirstOrDefaultAsync<EventoCategoria>(
                "SELECT * FROM Evento_Categorias WHERE CategoriaId = @CategoriaId", new { CategoriaId = CategoriaId });

            if (CategoriaExistente == null)
            {
                response.Mensagem = "Categoria não encontrada.";
                response.Status = true;
                return response;
            }

            await connection.ExecuteAsync(
                "DELETE FROM Evento_Categorias WHERE CategoriaId = @CategoriaId", new { CategoriaId = CategoriaId });

            var categorias = await connection.QueryAsync<EventoCategoria>("SELECT * FROM Evento_Categorias");
            response.Dados = _mapper.Map<List<EventoCategoriaDTO>>(categorias);
            response.Mensagem = "Categoria deletada com sucesso.";
            response.Status = true;

            return response;
        }
    }
}
