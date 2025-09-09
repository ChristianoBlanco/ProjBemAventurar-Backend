using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BemAventurar.Models
{
    public class Modulo
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
