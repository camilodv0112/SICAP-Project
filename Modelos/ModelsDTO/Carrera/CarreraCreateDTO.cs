using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.Carrera;

public class CarreraCreateDTO
{
    [Required, MaxLength(100)]
    public string Nombre { get; set; }
}
