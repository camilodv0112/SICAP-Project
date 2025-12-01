using Api.Data;
using Api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Modelos.Models;

namespace Api.Repositories
{
    public class CategoriaNecesidadRepository : Repository<CategoriaNecesidad>, ICategoriaNecesidadRepository
    {
        private readonly APIContext _context;

        public CategoriaNecesidadRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CategoriaNecesidad?> GetByNombreAsync(string nombre)
        {
            return await dbSet.FirstOrDefaultAsync(c => c.Nombre == nombre);
        }

        public async Task<List<CategoriaNecesidad>> GetAllWithSubcategoriasAsync()
        {
            return await dbSet.Include(c => c.Subcategorias).ToListAsync();
        }

        public async Task UpdateAsync(CategoriaNecesidad categoria)
        {
            dbSet.Update(categoria);
            await SaveChangesAsync();
        }
    }
}
