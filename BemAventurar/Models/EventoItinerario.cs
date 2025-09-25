using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BemAventurar.Models
{
    public class EventoItinerario
    {
        public int ItinerarioID { get; set; }

        [Required]
        public int? EventoID { get; set; }

        [Required]
        public string? Itinerario { get; set; }

        public DateTime CriadoEm { get; set; }
    }
}
