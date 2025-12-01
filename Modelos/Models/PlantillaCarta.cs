using System.ComponentModel.DataAnnotations;

namespace Modelos.Models;

public class PlantillaCarta
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string Nombre { get; set; }

    [Required]
    public string Contenido { get; set; }

    public ICollection<CartaGenerada> CartasGeneradas { get; set; }
}
