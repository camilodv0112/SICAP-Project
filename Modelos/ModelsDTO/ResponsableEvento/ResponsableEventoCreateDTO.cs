using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.ResponsableEvento
{
    public class ResponsableEventoCreateDTO
    {
        [Required]
        public int EventoID { get; set; }

        [Required]
        public int PersonalID { get; set; }
    }
}
