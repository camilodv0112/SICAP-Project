using System.ComponentModel.DataAnnotations;

namespace Modelos.Models;

public class Departamento
{
    [Key]
    public int DepartamentoID { get; set; }

    [Required, MaxLength(100)]
    public string Nombre { get; set; }

    public ICollection<Municipio> Municipios { get; set; }
}
