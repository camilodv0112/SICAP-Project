using Api.Data;
using Api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Modelos.Models;

namespace Api.Repositories
{
    public class PersonalRepository : Repository<Personal>, IPersonalRepository
    {
        private readonly APIContext _context;

        public PersonalRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Personal?> GetByCedulaAsync(string cedula)
        {
            return await dbSet.FirstOrDefaultAsync(p => p.Cedula == cedula);
        }

        public async Task<Personal?> GetByNumeroEmpleadoAsync(string numeroEmpleado)
        {
            return await dbSet.FirstOrDefaultAsync(p => p.NumeroEmpleado == numeroEmpleado);
        }

        public async Task<List<Personal>> GetByCargoAsync(int cargoId)
        {
            return await dbSet.Where(p => p.Cargo == cargoId).ToListAsync();
        }

        public async Task<List<Personal>> GetByDisciplinaAsync(int disciplinaId)
        {
            return await dbSet.Where(p => p.Disciplina == disciplinaId).ToListAsync();
        }

        public async Task UpdateAsync(Personal personal)
        {
            dbSet.Update(personal);
            await SaveChangesAsync();
        }
    }
}
