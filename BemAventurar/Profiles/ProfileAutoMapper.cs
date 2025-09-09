using AutoMapper;
using BemAventurar.DTO;
using BemAventurar.Models;

namespace BemAventurar.Profiles
{
    public class ProfileAutoMapper :Profile
    {
        public ProfileAutoMapper()
        {
            //Transforma o Models\Usuario em DTO\UsuarioListaDTO
            CreateMap<Usuario, UsuarioListarDTO>();
        }
    }
}
