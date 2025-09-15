using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BemAventurar.Models
{
    public class Inscricao
    {
        [Key]
        public int InscricaoId { get; set; }

        [Required]
        public int? EventoID { get; set; }

        [Required]
        public string? Cliente { get; set; }

        [Required]
        public int? Cpf { get; set; }

        [Required]
        public int? Tel { get; set; }

        [Required]
        public int? Telzap { get; set; }

        [Required]
        public DateTime? Data_nasc { get; set; }

        [Required]
        public string? Num_pessoas { get; set; }

        [Required]
        public string? Obs { get; set; }

        [Required]
        public DateTime? Data_insc { get; set; }

        [Required]
        public string? Status { get; set; }

        [Required]
        public DateTime CriadoEm { get; set; }
    }
}
