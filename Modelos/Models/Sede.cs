using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos.Models;

public class Sede
{
    [Key]
    public int SedeID { get; set; }

    [Required, MaxLength(150)]
    public string Nombre { get; set; }

    [MaxLength(255)]
    public string Descripcion { get; set; }

    [ForeignKey("Municipio")]
    public int MunicipioID { get; set; }
    public Municipio Municipio { get; set; }

    public ICollection<Salon> Salones { get; set; }
}
