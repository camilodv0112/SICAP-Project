using Api.Data;
using Api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Modelos.Models;

namespace Api.Repositories
{
    public class SubcategoriaNecesidadRepository : Repository<SubcategoriaNecesidad>, ISubcategoriaNecesidadRepository
    {
        private readonly APIContext _context;

        public SubcategoriaNecesidadRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<SubcategoriaNecesidad>> GetByCategoriaAsync(int categoriaId)
        {
            return await dbSet.Where(s => s.CategoriaID == categoriaId).ToListAsync();
        }

        public async Task UpdateAsync(SubcategoriaNecesidad subcategoria)
        {
            dbSet.Update(subcategoria);
            await SaveChangesAsync();
        }
    }
}
