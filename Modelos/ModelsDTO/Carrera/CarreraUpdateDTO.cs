using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.Carrera;

public class CarreraUpdateDTO
{
    [Required]
    public int CarreraID { get; set; }

    [Required, MaxLength(100)]
    public string Nombre { get; set; }
}
