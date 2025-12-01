using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos.Models;

public class Salon
{
    [Key]
    public int SalonID { get; set; }

    [Required, MaxLength(100)]
    public string Nombre { get; set; }

    [Required]
    public int Capacidad { get; set; }

    [MaxLength(255)]
    public string Descripcion { get; set; }

    [ForeignKey("Sede")]
    public int SedeID { get; set; }
    public Sede Sede { get; set; }

    public ICollection<Evento> Eventos { get; set; }
}
