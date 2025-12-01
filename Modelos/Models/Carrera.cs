using System.ComponentModel.DataAnnotations;

namespace Modelos.Models;

public class Carrera
{
    [Key]
    public int CarreraID { get; set; }

    [Required, MaxLength(100)]
    public string Nombre { get; set; }

    public ICollection<Estudiante> Estudiantes { get; set; }
}
