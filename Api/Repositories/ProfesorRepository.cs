using Api.Data;
using Api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Modelos.Models;

namespace Api.Repositories
{
    public class ProfesorRepository : Repository<Profesor>, IProfesorRepository
    {
        private readonly APIContext _context;

        public ProfesorRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Profesor>> GetByAsignaturaAsync(string asignatura)
        {
            return await dbSet.Where(p => p.Asignatura == asignatura).ToListAsync();
        }

        public async Task<Profesor?> GetWithEstudiantesAsync(int profesorId)
        {
            return await dbSet
                .Include(p => p.Estudiantes)
                .FirstOrDefaultAsync(p => p.ProfesorID == profesorId);
        }

        public async Task UpdateAsync(Profesor profesor)
        {
            dbSet.Update(profesor);
            await SaveChangesAsync();
        }
    }
}
