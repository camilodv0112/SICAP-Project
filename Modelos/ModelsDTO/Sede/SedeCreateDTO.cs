using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.Sede;

public class SedeCreateDTO
{
    [Required, MaxLength(150)]
    public string Nombre { get; set; }

    [MaxLength(255)]
    public string Descripcion { get; set; }

    [Required]
    public int MunicipioID { get; set; }
}
