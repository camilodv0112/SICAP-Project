using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.Salon;

public class SalonCreateDTO
{
    [Required, MaxLength(100)]
    public string Nombre { get; set; }

    [Required]
    public int Capacidad { get; set; }

    [MaxLength(255)]
    public string Descripcion { get; set; }

    [Required]
    public int SedeID { get; set; }
}
