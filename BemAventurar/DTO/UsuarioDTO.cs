using System.ComponentModel.DataAnnotations;

namespace BemAventurar.DTO
{
    public class UsuarioDTO
    {
        public int ClienteId { get; set; }

        [Required]
        public string? Nome { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Senha { get; set; }

        [Required]
        public DateTime CriadoEm { get; set; }

    }
}
 