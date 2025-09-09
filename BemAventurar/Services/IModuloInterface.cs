using BemAventurar.DTO;
using BemAventurar.Models;

namespace BemAventurar.Services
{
    public interface IModuloInterface
    {
        Task<ResponseModel<List<ModuloDTO>>> ListarModulos();
        Task<ResponseModel<List<ModuloDTO>>> PesquisarModulos(string? Nome_Modulo);
        Task<ResponseModel<List<ModuloDTO>>> CriarModulo(ModuloDTO moduloDto);
        Task<ResponseModel<List<ModuloDTO>>> EditarModulo(ModuloDTO moduloDto);
        Task<ResponseModel<List<ModuloDTO>>> ExcluirModulo(int moduloId);
    }
}
