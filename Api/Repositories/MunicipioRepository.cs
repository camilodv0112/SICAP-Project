using Api.Data;
using Api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Modelos.Models;

namespace Api.Repositories
{
    public class MunicipioRepository : Repository<Municipio>, IMunicipioRepository
    {
        private readonly APIContext _context;

        public MunicipioRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Municipio>> GetByDepartamentoAsync(int departamentoId)
        {
            return await dbSet.Where(m => m.DepartamentoID == departamentoId).ToListAsync();
        }

        public async Task<Municipio?> GetByNombreAsync(string nombre)
        {
            return await dbSet.FirstOrDefaultAsync(m => m.Nombre == nombre);
        }

        public async Task UpdateAsync(Municipio municipio)
        {
            dbSet.Update(municipio);
            await SaveChangesAsync();
        }
    }
}
