using Api.Data;
using Api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Modelos.Models;

namespace Api.Repositories
{
    public class EstudianteRepository : Repository<Estudiante>, IEstudianteRepository
    {
        private readonly APIContext _context;

        public EstudianteRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Estudiante?> GetByCedulaAsync(string cedula)
        {
            return await dbSet.FirstOrDefaultAsync(e => e.Cedula == cedula);
        }

        public async Task<Estudiante?> GetByCarnetAsync(string carnet)
        {
            return await dbSet.FirstOrDefaultAsync(e => e.Carnet == carnet);
        }

        public async Task<List<Estudiante>> GetByCarreraAsync(int carreraId)
        {
            return await dbSet.Where(e => e.Carrera == carreraId).ToListAsync();
        }

        public async Task<List<Estudiante>> GetByDisciplinaAsync(int disciplinaId)
        {
            return await dbSet.Where(e => e.Disciplina == disciplinaId).ToListAsync();
        }

        public async Task<Estudiante?> GetWithProfesoresAsync(int estudianteId)
        {
            return await dbSet
                .Include(e => e.Profesores)
                .FirstOrDefaultAsync(e => e.EstudianteID == estudianteId);
        }

        public async Task UpdateAsync(Estudiante estudiante)
        {
            dbSet.Update(estudiante);
            await SaveChangesAsync();
        }
    }
}
