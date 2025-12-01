using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.PlantillaCarta;

public class PlantillaCartaCreateDTO
{
    [Required, MaxLength(150)]
    public string Nombre { get; set; }

    [Required]
    public string Contenido { get; set; }
}
