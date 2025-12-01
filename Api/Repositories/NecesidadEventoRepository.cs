using Api.Data;
using Api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Modelos.Models;

namespace Api.Repositories
{
    public class NecesidadEventoRepository : Repository<NecesidadEvento>, INecesidadEventoRepository
    {
        private readonly APIContext _context;

        public NecesidadEventoRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<NecesidadEvento>> GetByEventoAsync(int eventoId)
        {
            return await dbSet.Where(n => n.EventoID == eventoId).ToListAsync();
        }

        public async Task<List<NecesidadEvento>> GetBySubcategoriaAsync(int subcategoriaId)
        {
            return await dbSet.Where(n => n.SubcategoriaID == subcategoriaId).ToListAsync();
        }

        public async Task UpdateAsync(NecesidadEvento necesidad)
        {
            dbSet.Update(necesidad);
            await SaveChangesAsync();
        }
    }
}
