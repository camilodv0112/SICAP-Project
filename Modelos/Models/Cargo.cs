using System.ComponentModel.DataAnnotations;

namespace Modelos.Models;

public class Cargo
{
    [Key]
    public int CargoID { get; set; }

    [Required, MaxLength(100)]
    public string Nombre { get; set; }

    [Required, MaxLength(255)]
    public string Descripcion { get; set; }

    public ICollection<Personal> Personales { get; set; }
}
