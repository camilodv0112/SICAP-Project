using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos.Models
{
    [Table("ResponsablesEventos")]
    public class ResponsableEvento
    {
        public int EventoID { get; set; }

        public int PersonalID { get; set; }

        [ForeignKey("EventoID")]
        public virtual Evento Evento { get; set; }

        [ForeignKey("PersonalID")]
        public virtual Personal Personal { get; set; }
    }
}
