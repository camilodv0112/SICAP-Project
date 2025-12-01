using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos.Models
{
    [Table("ParticipantesEventos")]
    public class ParticipanteEvento
    {
        public int EventoID { get; set; }

        public int EstudianteID { get; set; }

        [ForeignKey("EventoID")]
        public virtual Evento Evento { get; set; }

        [ForeignKey("EstudianteID")]
        public virtual Estudiante Estudiante { get; set; }
    }
}
