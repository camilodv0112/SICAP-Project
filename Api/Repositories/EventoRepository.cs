using Api.Data;
using Api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Modelos.Models;

namespace Api.Repositories
{
    public class EventoRepository : Repository<Evento>, IEventoRepository
    {
        private readonly APIContext _context;

        public EventoRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Evento>> GetBySalonAsync(int salonId)
        {
            return await dbSet.Where(e => e.Salon == salonId).ToListAsync();
        }

        public async Task<List<Evento>> GetByFechaAsync(DateTime fecha)
        {
            return await dbSet.Where(e => e.Fecha.Date == fecha.Date).ToListAsync();
        }

        public async Task<Evento?> GetWithParticipantesAsync(int eventoId)
        {
            return await dbSet
                .Include(e => e.Participantes)
                .FirstOrDefaultAsync(e => e.EventoID == eventoId);
        }

        public async Task<Evento?> GetWithResponsablesAsync(int eventoId)
        {
            return await dbSet
                .Include(e => e.Responsables)
                .FirstOrDefaultAsync(e => e.EventoID == eventoId);
        }

        public async Task UpdateAsync(Evento evento)
        {
            dbSet.Update(evento);
            await SaveChangesAsync();
        }
    }
}
