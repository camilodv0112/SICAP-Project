using Api.Data;
using Api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Modelos.Models;

namespace Api.Repositories
{
    public class CargoRepository : Repository<Cargo>, ICargoRepository
    {
        private readonly APIContext _context;

        public CargoRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Cargo?> GetByNombreAsync(string nombre)
        {
            return await dbSet.FirstOrDefaultAsync(c => c.Nombre == nombre);
        }

        public async Task UpdateAsync(Cargo cargo)
        {
            dbSet.Update(cargo);
            await SaveChangesAsync();
        }
    }
}
