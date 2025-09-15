using BemAventurar.DTO;
using BemAventurar.Models;

namespace BemAventurar.Services
{
    public interface IInscricaoInterface
    {
        Task<ResponseModel<List<InscricaoDTO>>> ListarInscricoes();
        Task<ResponseModel<List<InscricaoDTO>>> PesquisarInscricoes(string? Cliente, string?Cpf, string?Email);
        Task<ResponseModel<List<InscricaoDTO>>> CriarInscricao(InscricaoDTO inscricaoDto);
        Task<ResponseModel<List<InscricaoDTO>>> EditarInscricao(InscricaoDTO inscricaoDto);
        Task<ResponseModel<List<InscricaoDTO>>> ExcluirInscricao(int inscricaoId);
    }
}
