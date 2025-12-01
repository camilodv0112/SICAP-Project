using Modelos.Models;

namespace Api.Repositories.IRepositories;

public interface IEventoRepository : IRepository<Evento>
{
    Task<List<Evento>> GetBySalonAsync(int salonId);
    Task<List<Evento>> GetByFechaAsync(DateTime fecha);
    Task<Evento?> GetWithParticipantesAsync(int eventoId);
    Task<Evento?> GetWithResponsablesAsync(int eventoId);
    Task UpdateAsync(Evento evento);
}
