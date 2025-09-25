using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BemAventurar.Models
{
    public class EventoVideo
    {
        public int VideoID { get; set; }

        [Required]
        public int? EventoID { get; set; }

        [Required]
        public string? nome_video { get; set; }


        [Required]
        public string? Link_video { get; set; }

        public DateTime CriadoEm { get; set; }
    }
}
