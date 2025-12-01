using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.ParticipanteEvento
{
    public class ParticipanteEventoCreateDTO
    {
        [Required]
        public int EventoID { get; set; }

        [Required]
        public int EstudianteID { get; set; }
    }
}
