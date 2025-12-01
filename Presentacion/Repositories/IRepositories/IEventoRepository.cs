using Modelos.ModelsDTO.Evento;

namespace Presentacion.Repositories.IRepositories
{
    public interface IEventoRepository : IRepository<EventoResponseDTO>
    {
        Task<IEnumerable<EventoResponseDTO>> GetBySalonAsync(int salonId);
        Task<IEnumerable<EventoResponseDTO>> GetByFechaAsync(System.DateTime fecha);
        Task<EventoResponseDTO> GetWithParticipantesAsync(int id);
        Task<EventoResponseDTO> GetWithResponsablesAsync(int id);
    }
}
