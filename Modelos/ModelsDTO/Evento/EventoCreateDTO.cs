using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.Evento;

public class EventoCreateDTO
{
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

    [Required]
    public int Salon { get; set; }
}
