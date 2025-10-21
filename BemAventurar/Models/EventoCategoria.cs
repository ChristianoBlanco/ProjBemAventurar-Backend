using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BemAventurar.Models
{
    public class EventoCategoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [Required]
        public int? Nome_Categoria { get; set; }

        public DateTime CriadoEm { get; set; }
    }
}
