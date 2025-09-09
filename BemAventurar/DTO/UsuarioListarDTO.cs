using System.ComponentModel.DataAnnotations;

namespace BemAventurar.DTO
{
    public class UsuarioListarDTO //Na classe UsuarioListarDTO não tem a necessidade de listar o campo senha da tabela Usuario.
    {
        public int ClienteId { get; set; }

        [Required]
        public string? Nome { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public DateTime CriadoEm { get; set; }
    }
}
