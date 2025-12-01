using Api.Data;
using Api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Modelos.Models;

namespace Api.Repositories
{
    public class UsuarioEstudianteRepository : Repository<UsuarioEstudiante>, IUsuarioEstudianteRepository
    {
        private readonly APIContext _context;

        public UsuarioEstudianteRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UsuarioEstudiante?> GetByUsuarioAsync(string usuario)
        {
            return await dbSet.FirstOrDefaultAsync(u => u.Usuario == usuario);
        }

        public async Task<List<UsuarioEstudiante>> GetByEstudianteIdAsync(int estudianteId)
        {
            return await dbSet.Where(u => u.Estudiante == estudianteId).ToListAsync();
        }

        public async Task<List<UsuarioEstudiante>> GetByEstadoAsync(int estadoId)
        {
            return await dbSet.Where(u => u.Estado == estadoId).ToListAsync();
        }

        public async Task UpdateAsync(UsuarioEstudiante usuarioEstudiante)
        {
            dbSet.Update(usuarioEstudiante);
            await SaveChangesAsync();
        }
    }
}
