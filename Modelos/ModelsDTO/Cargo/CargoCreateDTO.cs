using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.Cargo;

public class CargoCreateDTO
{
    [Required, MaxLength(100)]
    public string Nombre { get; set; }

    [Required, MaxLength(255)]
    public string Descripcion { get; set; }
}
