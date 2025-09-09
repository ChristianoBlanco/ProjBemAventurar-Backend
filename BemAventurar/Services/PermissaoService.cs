using AutoMapper;
using BemAventurar.DTO;
using BemAventurar.Models;
using Dapper;
using System.Data.SqlClient;

namespace BemAventurar.Services
{
    public class PermissaoService : IPermissaoInterface
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        //Variáveis de construção
        public PermissaoService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        //Método criado para atualizar índice quando hover uma operação de INCLUIR , ALTERAR ou EXCLUIR 
        private static async Task<IEnumerable<Modulo>> ListarPermissoes(SqlConnection connection)
        {
            return await connection.QueryAsync<Modulo>("select * from Permissoes");
        }

        public async Task<ResponseModel<List<PermissaoDTO>>> ListarPermissoes()
        {
            var response = new ResponseModel<List<PermissaoDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var permissoes = await connection.QueryAsync<Permissao>("SELECT * FROM Permissoes");

                var permissoesMapeados = _mapper.Map<List<PermissaoDTO>>(permissoes);

                response.Dados = permissoesMapeados;
                response.Status = true;
                response.Mensagem = permissoesMapeados.Any() ? "Permissões encontradas." : "Nenhuma Permissão cadastrada.";
            }

            return response;
        }

        public async Task<ResponseModel<List<PermissaoDTO>>> PesquisarPermissoes(int? ClienteID)
        {
            var response = new ResponseModel<List<PermissaoDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sql = "SELECT * FROM Permissoes WHERE 1=1";

                var parametros = new DynamicParameters();

                if (ClienteID.HasValue)
                {
                    sql += " AND ClienteID LIKE @ClienteID";
                    parametros.Add("ClienteID", $"%{ClienteID}%");
                }

                var permissoes = await connection.QueryAsync<Permissao>(sql, parametros);

                var permissoesMapeados = _mapper.Map<List<PermissaoDTO>>(permissoes);

                response.Dados = permissoesMapeados;
                response.Status = true;
                response.Mensagem = permissoesMapeados.Any() ? "Permissão encontrada." : "Nenhuma Permissão encontrada.";
            }

            return response;
        }
        public async Task<ResponseModel<List<PermissaoDTO>>> CriarPermissao(PermissaoDTO permissaoDto)
        {
            ResponseModel<List<PermissaoDTO>> response = new ResponseModel<List<PermissaoDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))

            {
                // Parâmetros da tabela 
                var parametros = new
                {
                    ClienteID = permissaoDto.ClienteID,
                    ModuloID = permissaoDto.ModuloID,
                    Permitir = permissaoDto.Permitir,
                    CriadoEm = DateTime.UtcNow
                };

                var ModulosBanco = await connection.ExecuteAsync("insert into Permissoes (ClienteID, ModuloID, Permitir, CriadoEm) " +
                                                                    "values (@ClienteID, @ModuloID, @Permitir, @CriadoEm)", parametros);

                if (ModulosBanco == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao realizar o registro!";
                    response.Status = false;
                    return response;
                }

                var Permissoes = await ListarPermissoes(connection);

                var PermissoesMapeados = _mapper.Map<List<PermissaoDTO>>(Permissoes);

                response.Dados = PermissoesMapeados;
                response.Mensagem = "Permissão Inserida com sucesso!";
            }

            return response;
        }
        public async Task<ResponseModel<List<PermissaoDTO>>> EditarPermissao(PermissaoDTO permissaoDto)
        {
            var response = new ResponseModel<List<PermissaoDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                // Busca o Módulo atual
                var usuarioExistente = await connection.QueryFirstOrDefaultAsync<Permissao>(
                    "SELECT * FROM Permissoes WHERE PermissaoId = @PermissaoId",
                    new { PermissaoId = permissaoDto.PermissaoId });

                if (usuarioExistente == null)
                {
                    response.Mensagem = "Permissão não encontrada.";
                    response.Status = false;
                    return response;
                }
                var parametros = new
                {
                    ClienteID = permissaoDto.ClienteID,
                    ModuloId = permissaoDto.ModuloID,
                    Permitir = permissaoDto.Permitir

                };

                var atualizou = await connection.ExecuteAsync(
                    "UPDATE Permissoes SET Nome_Modulo = @Nome_Modulo, Link_Modulo = @Link_Modulo WHERE PermissaoId = @PermissaoId",
                    parametros);

                if (atualizou == 0)
                {
                    response.Mensagem = "Não foi possível atualizar a Permissão.";
                    response.Status = false;
                    return response;
                }

                //Faz o index pós a alteração dos registros
                var Permissoes = await ListarPermissoes(connection);
                var PermissoesMapeados = _mapper.Map<List<PermissaoDTO>>(Permissoes);

                response.Dados = PermissoesMapeados;
                response.Mensagem = "Permissão atualizada com sucesso!";
            }

            return response;
        }
        public async Task<ResponseModel<List<PermissaoDTO>>> ExcluirPermissao(int permissaoId)
        {
            var response = new ResponseModel<List<PermissaoDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var PermissaoExistente = await connection.QueryFirstOrDefaultAsync<Usuario>(
                    "SELECT * FROM Permissoes WHERE PermissaoId = @PermissaoId", new { PermissaoId = permissaoId });

                if (PermissaoExistente == null)
                {
                    response.Status = false;
                    response.Mensagem = "Permissão não encontrada.";
                    return response;
                }

                var deletado = await connection.ExecuteAsync(
                    "DELETE FROM Permissoes WHERE PermissaoId = @PermissaoId", new { PermissaoId = permissaoId });

                if (deletado == 0)
                {
                    response.Status = false;
                    response.Mensagem = "Não foi possível deletar a Permissão.";
                    return response;
                }

                //Faz o index pós a deleção dos registros
                var Permissoes = await ListarPermissoes(connection);
                var PermissoesMapeados = _mapper.Map<List<PermissaoDTO>>(Permissoes);

                response.Dados = PermissoesMapeados;
                response.Status = true;
                response.Mensagem = "Permissão deletada com sucesso!";
                return response;
            }
        }
    }
}
