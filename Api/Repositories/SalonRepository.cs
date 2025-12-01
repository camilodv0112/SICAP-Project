using Api.Data;
using Api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Modelos.Models;

namespace Api.Repositories
{
    public class SalonRepository : Repository<Salon>, ISalonRepository
    {
        private readonly APIContext _context;

        public SalonRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Salon>> GetBySedeAsync(int sedeId)
        {
            return await dbSet.Where(s => s.SedeID == sedeId).ToListAsync();
        }

        public async Task<List<Salon>> GetByCapacidadMinimaAsync(int capacidadMinima)
        {
            return await dbSet.Where(s => s.Capacidad >= capacidadMinima).ToListAsync();
        }

        public async Task UpdateAsync(Salon salon)
        {
            dbSet.Update(salon);
            await SaveChangesAsync();
        }
    }
}
