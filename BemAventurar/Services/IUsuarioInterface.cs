using System.Threading.Tasks;
using BemAventurar.DTO;
using BemAventurar.Models;

namespace BemAventurar.Services
{
    public interface IUsuarioInterface
    {
        Task<Usuario?> AutenticarUsuarioAsync(string email, string senha); //Método de autenticar 
        Task InicializarAdminAsync(); //InicializarAdminAsync é o método caso a tabela Usuario esteja vazia e faz um INSERT

        Task<ResponseModel<List<UsuarioListarDTO>>> CriarUsuario(UsuarioDTO usuarioDto);
        Task<ResponseModel<List<UsuarioListarDTO>>> EditarUsuario(UsuarioDTO usuarioDto);
        Task<ResponseModel<List<UsuarioListarDTO>>> PesquisarUsuarios(string? nome, string? email, int? clienteId);
        Task<ResponseModel<List<UsuarioListarDTO>>> ListarUsuarios();
        Task<ResponseModel<List<UsuarioListarDTO>>> ExcluirUsuario(int clienteId);



    }
}
