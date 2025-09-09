using System.ComponentModel.DataAnnotations;

namespace BemAventurar.DTO
{
    public class PermissaoDTO
    {
        [Key]
        public int PermissaoId { get; set; }

        [Required]
        public int? ClienteID { get; set; }

        [Required]
        public int? ModuloID { get; set; }

        [Required]
        public int? Permitir { get; set; }

        [Required]
        public DateTime CriadoEm { get; set; }
    }
}
