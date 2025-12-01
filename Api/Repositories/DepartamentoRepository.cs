using Api.Data;
using Api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Modelos.Models;

namespace Api.Repositories
{
    public class DepartamentoRepository : Repository<Departamento>, IDepartamentoRepository
    {
        private readonly APIContext _context;

        public DepartamentoRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Departamento?> GetByNombreAsync(string nombre)
        {
            return await dbSet.FirstOrDefaultAsync(d => d.Nombre == nombre);
        }

        public async Task<List<Departamento>> GetAllWithMunicipiosAsync()
        {
            return await dbSet.Include(d => d.Municipios).ToListAsync();
        }

        public async Task UpdateAsync(Departamento departamento)
        {
            dbSet.Update(departamento);
            await SaveChangesAsync();
        }
    }
}
