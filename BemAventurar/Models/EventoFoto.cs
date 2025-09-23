using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BemAventurar.Models
{
    public class EventoFoto
    {
        public int FotoId { get; set; }

        [Required]
        public int? EventoID { get; set; }

        [Required]
        public string? Nome_foto { get; set; }

        [Required]
        public string? Link_foto { get; set; }

        public DateTime CriadoEm { get; set; }
    }
}
