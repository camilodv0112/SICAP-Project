using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos.Models;

public class Evento
{
    [Key]
    public int EventoID { get; set; }

    [Required, MaxLength(150)]
    public string Nombre { get; set; }

    [MaxLength(300)]
    public string Descripcion { get; set; }

    [Required]
    public DateTime Fecha { get; set; }

    [Required]
    public TimeSpan HoraInicio { get; set; }

    [Required]
    public TimeSpan HoraFinal { get; set; }

    [ForeignKey("SalonNavigation")]
    public int Salon { get; set; }
    public Salon SalonNavigation { get; set; }

    public ICollection<NecesidadEvento> Necesidades { get; set; }
    public ICollection<Estudiante> Participantes { get; set; }
    public ICollection<Personal> Responsables { get; set; }
}
