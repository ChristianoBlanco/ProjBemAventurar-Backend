using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BemAventurar.DTO
{
    public class EventoFaqDTO
    {
        [Key]
        public int FaqId { get; set; }

        [Required]
        public int? EventoID { get; set; }

        [Required]
        public string? Pergunta_faq { get; set; }

        [Required]
        public string? Resposta_faq { get; set; }

        public DateTime CriadoEm { get; set; }
    }
}
