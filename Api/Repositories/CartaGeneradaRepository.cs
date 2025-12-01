using Api.Data;
using Api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Modelos.Models;

namespace Api.Repositories
{
    public class CartaGeneradaRepository : Repository<CartaGenerada>, ICartaGeneradaRepository
    {
        private readonly APIContext _context;

        public CartaGeneradaRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<CartaGenerada>> GetByPlantillaAsync(int plantillaId)
        {
            return await dbSet.Where(c => c.PlantillaId == plantillaId).ToListAsync();
        }

        public async Task<List<CartaGenerada>> GetByUsuarioAsync(int usuarioPersonalId)
        {
            return await dbSet.Where(c => c.UsuarioPersonalID == usuarioPersonalId).ToListAsync();
        }

        public async Task<List<CartaGenerada>> GetByFechaAsync(DateTime fecha)
        {
            return await dbSet.Where(c => c.FechaGeneracion.HasValue && 
                c.FechaGeneracion.Value.Date == fecha.Date).ToListAsync();
        }

        public async Task UpdateAsync(CartaGenerada carta)
        {
            dbSet.Update(carta);
            await SaveChangesAsync();
        }
    }
}
