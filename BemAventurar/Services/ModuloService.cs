using AutoMapper;
using BemAventurar.DTO;
using BemAventurar.Models;
using Dapper;
using System.Data.SqlClient;

namespace BemAventurar.Services
{
    public class ModuloService : IModuloInterface
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        //Variáveis de construção
        public ModuloService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        //Método criado para atualizar índice quando hover uma operação de INCLUIR , ALTERAR ou EXCLUIR 
        private static async Task<IEnumerable<Modulo>> ListarModulos(SqlConnection connection)
        {
            return await connection.QueryAsync<Modulo>("select * from Modulos");
        }

        //Método listar módulos
        public async Task<ResponseModel<List<ModuloDTO>>> ListarModulos()
        {
            var response = new ResponseModel<List<ModuloDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var modulos = await connection.QueryAsync<Modulo>("SELECT * FROM Modulos");

                var modulosMapeados = _mapper.Map<List<ModuloDTO>>(modulos);

                response.Dados = modulosMapeados;
                response.Status = true;
                response.Mensagem = modulosMapeados.Any() ? "Módulos encontrados." : "Nenhum Módulo cadastrado.";
            }

            return response;
        }

        public async Task<ResponseModel<List<ModuloDTO>>> PesquisarModulos(string? Nome_Modulo)
        {
            var response = new ResponseModel<List<ModuloDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sql = "SELECT * FROM Modulos WHERE 1=1";

                var parametros = new DynamicParameters();

                if (!string.IsNullOrWhiteSpace(Nome_Modulo))
                {
                    sql += " AND Nome_Modulo LIKE @Nome";
                    parametros.Add("Nome_Modulo", $"%{Nome_Modulo}%");
                }

                var modulos = await connection.QueryAsync<Modulo>(sql, parametros);

                var modulosMapeados = _mapper.Map<List<ModuloDTO>>(modulos);

                response.Dados = modulosMapeados;
                response.Status = true;
                response.Mensagem = modulosMapeados.Any() ? "Módulo encontrado." : "Nenhum Módulo encontrado.";
            }

            return response;
        }

        public async Task<ResponseModel<List<ModuloDTO>>> CriarModulo(ModuloDTO moduloDto)
        {
            ResponseModel<List<ModuloDTO>> response = new ResponseModel<List<ModuloDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))

            {
                // Parâmetros da tabela módulos
                var parametros = new
                {
                    Nome_Modulo = moduloDto.Nome_Modulo,
                    Link_Modulo = moduloDto.Link_Modulo,
                    CriadoEm = DateTime.UtcNow
                };

                var ModulosBanco = await connection.ExecuteAsync("insert into Modulos (Nome_Modulo, Link_Modulo,CriadoEm) " +
                                                                    "values (@Nome_Modulo, @Link_Modulo, @CriadoEm)", parametros);

                if (ModulosBanco == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao realizar o registro!";
                    response.Status = false;
                    return response;
                }

                var modulos = await ListarModulos(connection);

                var modulosMapeados = _mapper.Map<List<ModuloDTO>>(modulos);

                response.Dados = modulosMapeados;
                response.Mensagem = "Módulo Inserido com sucesso!";
            }

            return response;
        }

        public async Task<ResponseModel<List<ModuloDTO>>> EditarModulo(ModuloDTO moduloDto)
        {
            var response = new ResponseModel<List<ModuloDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                // Busca o Módulo atual
                var usuarioExistente = await connection.QueryFirstOrDefaultAsync<Modulo>(
                    "SELECT * FROM Modulos WHERE ModuloId = @ModuloId",
                    new { ModuloId = moduloDto.ModuloId });

                if (usuarioExistente == null)
                {
                    response.Mensagem = "Módulo não encontrado.";
                    response.Status = false;
                    return response;
                }
                var parametros = new
                {
                    Nome_Modulo = moduloDto.Nome_Modulo,
                    Link_Modulo = moduloDto.Link_Modulo,
                    ModuloId = moduloDto.ModuloId
                };

                var atualizou = await connection.ExecuteAsync(
                    "UPDATE Modulos SET Nome_Modulo = @Nome_Modulo, Link_Modulo = @Link_Modulo WHERE ModuloId = @ModuloId",
                    parametros);

                if (atualizou == 0)
                {
                    response.Mensagem = "Não foi possível atualizar o Módulo.";
                    response.Status = false;
                    return response;
                }

                //Faz o index pós a alteração dos registros
                var modulos = await ListarModulos(connection);
                var modulosMapeados = _mapper.Map<List<ModuloDTO>>(modulos);

                response.Dados = modulosMapeados;
                response.Mensagem = "Módulo atualizado com sucesso!";
            }

            return response;
        }

        public async Task<ResponseModel<List<ModuloDTO>>> ExcluirModulo(int moduloId)
        {
            var response = new ResponseModel<List<ModuloDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var moduloExistente = await connection.QueryFirstOrDefaultAsync<Usuario>(
                    "SELECT * FROM Modulos WHERE ModuloId = @ModuloId", new { ModuloId = moduloId });

                if (moduloExistente == null)
                {
                    response.Status = false;
                    response.Mensagem = "Módulo não encontrado.";
                    return response;
                }

                var deletado = await connection.ExecuteAsync(
                    "DELETE FROM Modulos WHERE ModuloId = @ModuloId", new { ModuloId = moduloId });

                if (deletado == 0)
                {
                    response.Status = false;
                    response.Mensagem = "Não foi possível deletar o Módulo.";
                    return response;
                }

                //Faz o index pós a deleção dos registros
                var modulos = await ListarModulos(connection);
                var modulosMapeados = _mapper.Map<List<ModuloDTO>>(modulos);

                response.Dados = modulosMapeados;
                response.Status = true;
                response.Mensagem = "Módulo deletado com sucesso!";
                return response;
            }
        }
    }
}
