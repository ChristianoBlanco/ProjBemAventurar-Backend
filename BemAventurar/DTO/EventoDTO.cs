using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BemAventurar.DTO
{
    public class EventoDTO
    {
        [Key]
        public int EventoId { get; set; }

        [Required]
        public string? Nome_evento { get; set; }

        [Required]
        public string? Desc_evento { get; set; }

        [Required]
        public string? Sobre_evento { get; set; }

        [Required]
        public string? Local_evento { get; set; }

        [Required]
        public string? Material_evento { get; set; }

        [Required]
        public decimal Preco_evento { get; set; }

        [Required]
        public DateTime? Data_evento { get; set; }

        [Required]
        public DateTime CriadoEm { get; set; }
    }
}
