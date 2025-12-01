using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos.Models;

public class CartaGenerada
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Plantilla")]
    public int PlantillaId { get; set; }
    public PlantillaCarta Plantilla { get; set; }

    [Required, MaxLength(150)]
    public string NombreCarta { get; set; }

    public string Datos { get; set; }

    [MaxLength(255)]
    public string RutaPDF { get; set; }

    [ForeignKey("UsuarioPersonal")]
    public int UsuarioPersonalID { get; set; }
    public UsuarioPersonal UsuarioPersonal { get; set; }

    public DateTime? FechaGeneracion { get; set; } = DateTime.Now;
}
