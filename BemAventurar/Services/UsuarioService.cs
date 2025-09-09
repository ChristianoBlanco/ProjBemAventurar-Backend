using AutoMapper;
using BemAventurar.DTO;
using BemAventurar.Models;
using Dapper;
using System.Data.SqlClient;

namespace BemAventurar.Services
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        //Variáveis de construção
        public UsuarioService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        //Método criado para atualizar índice quando hover uma operação de INCLUIR , ALTERAR ou EXCLUIR 
        private static async Task<IEnumerable<Usuario>> ListarUsuarios(SqlConnection connection)
        {

            return await connection.QueryAsync<Usuario>("select * from Usuarios");

        }

        //Método listar usuários
        public async Task<ResponseModel<List<UsuarioListarDTO>>> ListarUsuarios()
        {
            var response = new ResponseModel<List<UsuarioListarDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")                                                             ))
            {
                var usuarios = await connection.QueryAsync<Usuario>("SELECT * FROM Usuarios");

                var usuariosMapeados = _mapper.Map<List<UsuarioListarDTO>>(usuarios);

                response.Dados = usuariosMapeados;
                response.Status = true;
                response.Mensagem = usuariosMapeados.Any() ? "Usuários encontrados." : "Nenhum usuário cadastrado.";
            }

            return response;
        }


        //Método pesquisar usuário por nome, e-mail ou id
        public async Task<ResponseModel<List<UsuarioListarDTO>>> PesquisarUsuarios(string? nome, string? email, int? clienteId)
        {
            var response = new ResponseModel<List<UsuarioListarDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sql = "SELECT * FROM Usuarios WHERE 1=1";

                var parametros = new DynamicParameters();

                if (!string.IsNullOrWhiteSpace(nome))
                {
                    sql += " AND Nome LIKE @Nome";
                    parametros.Add("Nome", $"%{nome}%");
                }

                if (!string.IsNullOrWhiteSpace(email))
                {
                    sql += " AND Email LIKE @Email";
                    parametros.Add("Email", $"%{email}%");
                }

                if (clienteId.HasValue)
                {
                    sql += " AND ClienteId = @ClienteId";
                    parametros.Add("ClienteId", clienteId.Value);
                }

                var usuarios = await connection.QueryAsync<Usuario>(sql, parametros);

                var usuariosMapeados = _mapper.Map<List<UsuarioListarDTO>>(usuarios);

                response.Dados = usuariosMapeados;
                response.Status = true;
                response.Mensagem = usuariosMapeados.Any() ? "Usuários encontrados." : "Nenhum usuário encontrado.";
            }

            return response;
        }


        //Método criar usuário
        public async Task<ResponseModel<List<UsuarioListarDTO>>> CriarUsuario(UsuarioDTO usuarioDto)
        {
            ResponseModel<List<UsuarioListarDTO>> response = new ResponseModel<List<UsuarioListarDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))

            {
                // Criptografar a senha usando BCrypt
                string senhaCriptografada = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Senha);

                // Parâmetros com senha já criptografada
                var parametros = new
                {
                    Nome = usuarioDto.Nome,
                    Email = usuarioDto.Email,
                    Senha = senhaCriptografada,
                    CriadoEm = DateTime.UtcNow
                };

                var usuariosBanco = await connection.ExecuteAsync("insert into Usuarios (Nome, Email, Senha, CriadoEm) " +
                                                                    "values (@Nome, @Email, @Senha, @CriadoEm)", parametros);

                if (usuariosBanco == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao realizar o registro!";
                    response.Status = false;
                    return response;
                }

                var usuarios = await ListarUsuarios(connection);

                var usuariosMapeados = _mapper.Map<List<UsuarioListarDTO>>(usuarios);

                response.Dados = usuariosMapeados;
                response.Mensagem = "Usuário Inserido com sucesso!";
            }

            return response;
        }

       
        //Método editar usuário
        public async Task<ResponseModel<List<UsuarioListarDTO>>> EditarUsuario(UsuarioDTO usuarioDto)
        {
            var response = new ResponseModel<List<UsuarioListarDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                // Busca o usuário atual
                var usuarioExistente = await connection.QueryFirstOrDefaultAsync<Usuario>(
                    "SELECT * FROM Usuarios WHERE ClienteId = @ClienteId",
                    new { ClienteId = usuarioDto.ClienteId });

                if (usuarioExistente == null)
                {
                    response.Mensagem = "Usuário não encontrado.";
                    response.Status = false;
                    return response;
                }

                // Se uma nova senha foi enviada, criptografa; senão, mantém a antiga
                string senhaFinal = string.IsNullOrWhiteSpace(usuarioDto.Senha)
                    ? usuarioExistente.Senha
                    : BCrypt.Net.BCrypt.HashPassword(usuarioDto.Senha);

                var parametros = new
                {
                    Nome = usuarioDto.Nome,
                    Email = usuarioDto.Email,
                    Senha = senhaFinal,
                    ClienteId = usuarioDto.ClienteId
                };

                var atualizou = await connection.ExecuteAsync(
                    "UPDATE Usuarios SET Nome = @Nome, Email = @Email, Senha = @Senha WHERE ClienteId = @ClienteId",
                    parametros);

                if (atualizou == 0)
                {
                    response.Mensagem = "Não foi possível atualizar o usuário.";
                    response.Status = false;
                    return response;
                }

                var usuarios = await ListarUsuarios(connection);
                var usuariosMapeados = _mapper.Map<List<UsuarioListarDTO>>(usuarios);

                response.Dados = usuariosMapeados;
                response.Mensagem = "Usuário atualizado com sucesso!";
            }

            return response;
        }

        //Método excluir usuário
        public async Task<ResponseModel<List<UsuarioListarDTO>>> ExcluirUsuario(int clienteId)
        {
            var response = new ResponseModel<List<UsuarioListarDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioExistente = await connection.QueryFirstOrDefaultAsync<Usuario>(
                    "SELECT * FROM Usuarios WHERE ClienteId = @ClienteId", new { ClienteId = clienteId });

                if (usuarioExistente == null)
                {
                    response.Status = false;
                    response.Mensagem = "Usuário não encontrado.";
                    return response;
                }

                var deletado = await connection.ExecuteAsync(
                    "DELETE FROM Usuarios WHERE ClienteId = @ClienteId", new { ClienteId = clienteId });

                if (deletado == 0)
                {
                    response.Status = false;
                    response.Mensagem = "Não foi possível deletar o usuário.";
                    return response;
                }

                var usuarios = await ListarUsuarios(connection);
                var usuariosMapeados = _mapper.Map<List<UsuarioListarDTO>>(usuarios);

                response.Dados = usuariosMapeados;
                response.Status = true;
                response.Mensagem = "Usuário deletado com sucesso!";
                return response;
            }
        }



        //Método para autenticar uauário
        [Obsolete]
        public async Task<Usuario?> AutenticarUsuarioAsync(string email, string senha)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            string query = "SELECT * FROM Usuarios WHERE Email = @Email";

            var usuario = await connection.QueryFirstOrDefaultAsync<Usuario>(query, new { Email = email });

            if (usuario == null)
                return null;

            bool senhaValida = BCrypt.Net.BCrypt.Verify(senha, usuario.Senha);
            return senhaValida ? usuario : null;
        }

        //Método para verificar na autenticação SE não existir usuário, é criado um admin como default.(Usuario:admin , Senha:abcd1234)
        [Obsolete]
        public async Task InicializarAdminAsync()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            string countQuery = "SELECT COUNT(*) FROM Usuarios";
            int total = await connection.ExecuteScalarAsync<int>(countQuery);

            if (total == 0)
            {
                string senhaCriptografada = BCrypt.Net.BCrypt.HashPassword("abcd1234");

                string insertQuery = @"
                    INSERT INTO Usuarios (Nome, Email, Senha, CriadoEm)
                    VALUES (@Nome, @Email, @Senha, @CriadoEm)";

                await connection.ExecuteAsync(insertQuery, new
                {
                    Nome = "admin",
                    Email = "christiano.blanco@hotmail.com",
                    Senha = senhaCriptografada,
                    CriadoEm = DateTime.UtcNow
                });
            }
        }
    }
}
