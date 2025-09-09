using System.ComponentModel.DataAnnotations;

namespace BemAventurar.DTO
{
    public class ModuloDTO
    {
        [Key]
        public int ModuloId { get; set; }

        [Required]
        public string? Nome_Modulo { get; set; }

        [Required]
        public string? Link_Modulo { get; set; }

        [Required]
        public DateTime CriadoEm { get; set; }
    }
}
