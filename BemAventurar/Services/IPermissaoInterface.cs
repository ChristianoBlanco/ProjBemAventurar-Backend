using BemAventurar.DTO;
using BemAventurar.Models;

namespace BemAventurar.Services
{
    public interface IPermissaoInterface
    {
        Task<ResponseModel<List<PermissaoDTO>>> ListarPermissoes();
        Task<ResponseModel<List<PermissaoDTO>>> PesquisarPermissoes(int? ClienteID);
        Task<ResponseModel<List<PermissaoDTO>>> CriarPermissao(PermissaoDTO permissaoDTO);
        Task<ResponseModel<List<PermissaoDTO>>> EditarPermissao(PermissaoDTO permissaoDTO);
        Task<ResponseModel<List<PermissaoDTO>>> ExcluirPermissao(int PermissaoId);
    }
}
