using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BemAventurar.Models
{
    public class Permissao
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
