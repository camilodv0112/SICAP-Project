using System.ComponentModel.DataAnnotations;

namespace Modelos.Models;

public class DisciplinaArtistica
{
    [Key]
    public int DisciplinaID { get; set; }

    [Required, MaxLength(100)]
    public string Nombre { get; set; }

    public ICollection<Estudiante> Estudiantes { get; set; }
    public ICollection<Personal> Personales { get; set; }
}
