using AutoMapper;
using BemAventurar.DTO;
using BemAventurar.Models;
using Dapper;
using System.Data.SqlClient;

namespace BemAventurar.Services
{
    public class InscricaoService : IInscricaoInterface
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        //Variáveis de construção
        public InscricaoService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }
        //Método criado para atualizar índice quando hover uma operação de INCLUIR , ALTERAR ou EXCLUIR 
        private static async Task<IEnumerable<Inscricao>> ListarInscricoes(SqlConnection connection)
        {
            return await connection.QueryAsync<Inscricao>("select * from Inscricoes");
        }
        public async Task<ResponseModel<List<InscricaoDTO>>> ListarInscricoes()
        {
            var response = new ResponseModel<List<InscricaoDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var inscricoes = await connection.QueryAsync<Modulo>("SELECT * FROM Inscricoes");

                var inscricoesMapeados = _mapper.Map<List<InscricaoDTO>>(inscricoes);

                response.Dados = inscricoesMapeados;
                response.Status = true;
                response.Mensagem = inscricoesMapeados.Any() ? "Inscrições cadastradas." : "Nenhuma Inscrição cadastrada.";
            }

            return response;
        }
        public async Task<ResponseModel<List<InscricaoDTO>>> PesquisarInscricoes(string?Cliente, string?Cpf, string?Email)
        {
            var response = new ResponseModel<List<InscricaoDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sql = "SELECT * FROM Inscricoes WHERE 1=1";

                var parametros = new DynamicParameters();

                if (!string.IsNullOrWhiteSpace(Cliente))
                {
                    sql += " AND Cliente LIKE @Cliente";
                    parametros.Add("Cliente", $"%{Cliente}%");
                }

                if (!string.IsNullOrWhiteSpace(Cpf))
                {
                    sql += " AND Cpf LIKE @Cpf";
                    parametros.Add("Cpf", $"%{Cpf}%");
                }

                if (!string.IsNullOrWhiteSpace(Email))
                {
                    sql += " AND Email LIKE @Email";
                    parametros.Add("Email", $"%{Email}%");
                }

                var Inscricoes = await connection.QueryAsync<Inscricao>(sql, parametros);

                var InscricoesMapeados = _mapper.Map<List<InscricaoDTO>>(Inscricoes);

                response.Dados = InscricoesMapeados;
                response.Status = true;
                response.Mensagem = InscricoesMapeados.Any() ? "Inscrições encontradas." : "Nenhuma inscrição encontrada.";
            }

            return response;
        }

        public async Task<ResponseModel<List<InscricaoDTO>>> CriarInscricao(InscricaoDTO inscricaoDto)
        {
            ResponseModel<List<InscricaoDTO>> response = new ResponseModel<List<InscricaoDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                // Verifica se já existe inscrição para o mesmo EventoID
                var inscricaoExistente = await connection.QueryFirstOrDefaultAsync<Inscricao>(
                    "SELECT * FROM Inscricoes WHERE EventoID = @EventoID",
                    new { EventoID = inscricaoDto.EventoID });

                if (inscricaoExistente == null)
                {
                    // Inserir nova inscrição
                    var parametros = new
                    {
                        EventoID = inscricaoDto.EventoID,
                        Cliente = inscricaoDto.Cliente,
                        Cpf = inscricaoDto.Cpf,
                        Tel = inscricaoDto.Tel,
                        Telzap = inscricaoDto.Telzap,
                        Data_nasc = inscricaoDto.Data_nasc,
                        Num_pessoas = inscricaoDto.Num_pessoas,
                        Obs = inscricaoDto.Obs,
                        Data_insc = inscricaoDto.Data_insc,
                        Status = inscricaoDto.Status,
                        CriadoEm = DateTime.UtcNow
                    };

                    var resultado = await connection.ExecuteAsync(@"INSERT INTO Inscricoes
                (EventoID, Cliente, Cpf, Tel, Telzap, Data_nasc, Num_pessoas, Obs, Data_insc, Status, CriadoEm)
                VALUES (@EventoID, @Cliente, @Cpf, @Tel, @Telzap, @Data_nasc, @Num_pessoas, @Obs, @Data_insc, @Status, @CriadoEm)", parametros);

                    if (resultado == 0)
                    {
                        response.Mensagem = "Ocorreu um erro ao realizar o registro!";
                        response.Status = false;
                        return response;
                    }
                }
                else
                {
                    // Atualizar inscrição existente
                    var parametros = new
                    {
                        InscricaoId = inscricaoExistente.InscricaoId,
                        EventoID = inscricaoDto.EventoID,
                        Cliente = inscricaoDto.Cliente,
                        Cpf = inscricaoDto.Cpf,
                        Tel = inscricaoDto.Tel,
                        Telzap = inscricaoDto.Telzap,
                        Data_nasc = inscricaoDto.Data_nasc,
                        Num_pessoas = inscricaoDto.Num_pessoas,
                        Obs = inscricaoDto.Obs,
                        Data_insc = inscricaoDto.Data_insc,
                        Status = inscricaoDto.Status,
                        CriadoEm = DateTime.UtcNow
                    };

                    var resultado = await connection.ExecuteAsync(@"UPDATE Inscricoes SET
                Cliente = @Cliente,
                Cpf = @Cpf,
                Tel = @Tel,
                Telzap = @Telzap,
                Data_nasc = @Data_nasc,
                Num_pessoas = @Num_pessoas,
                Obs = @Obs,
                Data_insc = @Data_insc,
                Status = @Status,
                CriadoEm = @CriadoEm
                WHERE EventoID = @EventoID", parametros);

                    if (resultado == 0)
                    {
                        response.Mensagem = "Ocorreu um erro ao atualizar o registro!";
                        response.Status = false;
                        return response;
                    }
                }

                // Buscar todas as inscrições após a operação
                var inscricoes = await connection.QueryAsync<Inscricao>("SELECT * FROM Inscricoes");
                var inscricoesMapeadas = _mapper.Map<List<InscricaoDTO>>(inscricoes);

                response.Dados = inscricoesMapeadas;
                response.Mensagem = "Inscrição salva com sucesso!";
                response.Status = true;
            }

            return response;
        }

        public async Task<ResponseModel<List<InscricaoDTO>>> EditarInscricao(InscricaoDTO inscricaoDto)
        {
            ResponseModel<List<InscricaoDTO>> response = new ResponseModel<List<InscricaoDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                // Verifica se a inscrição existe pelo InscricaoId
                var inscricaoExistente = await connection.QueryFirstOrDefaultAsync<Inscricao>(
                    "SELECT * FROM Inscricoes WHERE InscricaoId = @InscricaoId",
                    new { InscricaoId = inscricaoDto.InscricaoId });

                if (inscricaoExistente == null)
                {
                    response.Mensagem = "Inscrição não encontrada!";
                    response.Status = false;
                    return response;
                }

                var parametros = new
                {
                    InscricaoId = inscricaoDto.InscricaoId,
                    EventoID = inscricaoDto.EventoID,
                    Cliente = inscricaoDto.Cliente,
                    Cpf = inscricaoDto.Cpf,
                    Tel = inscricaoDto.Tel,
                    Telzap = inscricaoDto.Telzap,
                    Data_nasc = inscricaoDto.Data_nasc,
                    Num_pessoas = inscricaoDto.Num_pessoas,
                    Obs = inscricaoDto.Obs,
                    Data_insc = inscricaoDto.Data_insc,
                    Status = inscricaoDto.Status,
                    CriadoEm = DateTime.UtcNow
                };

                var resultado = await connection.ExecuteAsync(@"UPDATE Inscricoes SET
                EventoID = @EventoID,
                Cliente = @Cliente,
                Cpf = @Cpf,
                Tel = @Tel,
                Telzap = @Telzap,
                Data_nasc = @Data_nasc,
                Num_pessoas = @Num_pessoas,
                Obs = @Obs,
                Data_insc = @Data_insc,
                Status = @Status,
                CriadoEm = @CriadoEm
                WHERE InscricaoId = @InscricaoId", parametros);

                if (resultado == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao atualizar a inscrição!";
                    response.Status = false;
                    return response;
                }

                // Buscar todas as inscrições após a atualização
                var inscricoes = await connection.QueryAsync<Inscricao>("SELECT * FROM Inscricoes");
                var inscricoesMapeadas = _mapper.Map<List<InscricaoDTO>>(inscricoes);

                response.Dados = inscricoesMapeadas;
                response.Mensagem = "Inscrição atualizada com sucesso!";
                response.Status = true;
            }

            return response;
        }
        public async Task<ResponseModel<List<InscricaoDTO>>> ExcluirInscricao(int inscricaoId)
        {
            ResponseModel<List<InscricaoDTO>> response = new ResponseModel<List<InscricaoDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                // Verifica se a inscrição existe
                var inscricaoExistente = await connection.QueryFirstOrDefaultAsync<Inscricao>(
                    "SELECT * FROM Inscricoes WHERE InscricaoId = @InscricaoId",
                    new { InscricaoId = inscricaoId });

                if (inscricaoExistente == null)
                {
                    response.Mensagem = "Inscrição não encontrada!";
                    response.Status = false;
                    return response;
                }

                var resultado = await connection.ExecuteAsync(
                    "DELETE FROM Inscricoes WHERE InscricaoId = @InscricaoId",
                    new { InscricaoId = inscricaoId });

                if (resultado == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao excluir a inscrição!";
                    response.Status = false;
                    return response;
                }

                // Buscar todas as inscrições após a exclusão
                var inscricoes = await connection.QueryAsync<Inscricao>("SELECT * FROM Inscricoes");
                var inscricoesMapeadas = _mapper.Map<List<InscricaoDTO>>(inscricoes);

                response.Dados = inscricoesMapeadas;
                response.Mensagem = "Inscrição excluída com sucesso!";
                response.Status = true;
            }

            return response;
        }




    }
}
