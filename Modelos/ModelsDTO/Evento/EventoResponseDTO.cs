namespace Modelos.ModelsDTO.Evento;

public class EventoResponseDTO
{
    public int EventoID { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public DateTime Fecha { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFinal { get; set; }
    public string SalonNombre { get; set; }
    public string SedeNombre { get; set; }
    public int CantidadParticipantes { get; set; }
    public int CantidadResponsables { get; set; }
}
