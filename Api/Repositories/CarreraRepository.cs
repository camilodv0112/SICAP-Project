using Api.Data;
using Api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Modelos.Models;

namespace Api.Repositories
{
    public class CarreraRepository : Repository<Carrera>, ICarreraRepository
    {
        private readonly APIContext _context;

        public CarreraRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Carrera?> GetByNombreAsync(string nombre)
        {
            return await dbSet.FirstOrDefaultAsync(c => c.Nombre == nombre);
        }

        public async Task<List<Carrera>> GetAllWithEstudiantesAsync()
        {
            return await dbSet.Include(c => c.Estudiantes).ToListAsync();
        }

        public async Task UpdateAsync(Carrera carrera)
        {
            dbSet.Update(carrera);
            await SaveChangesAsync();
        }
    }
}
