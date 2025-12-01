using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos.Models;

public class Municipio
{
    [Key]
    public int MunicipioID { get; set; }

    [Required, MaxLength(100)]
    public string Nombre { get; set; }

    [ForeignKey("Departamento")]
    public int DepartamentoID { get; set; }
    public Departamento Departamento { get; set; }

    public ICollection<Sede> Sedes { get; set; }
    public ICollection<Estudiante> Estudiantes { get; set; }
    public ICollection<Personal> Personales { get; set; }
}
