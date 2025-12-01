using Api.Data;
using Api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Modelos.Models;

namespace Api.Repositories
{
    public class UsuarioPersonalRepository : Repository<UsuarioPersonal>, IUsuarioPersonalRepository
    {
        private readonly APIContext _context;

        public UsuarioPersonalRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UsuarioPersonal?> GetByUsuarioAsync(string usuario)
        {
            return await dbSet.FirstOrDefaultAsync(u => u.Usuario == usuario);
        }

        public async Task<List<UsuarioPersonal>> GetByPersonalIdAsync(int personalId)
        {
            return await dbSet.Where(u => u.Personal == personalId).ToListAsync();
        }

        public async Task<List<UsuarioPersonal>> GetByEstadoAsync(int estadoId)
        {
            return await dbSet.Where(u => u.Estado == estadoId).ToListAsync();
        }

        public async Task UpdateAsync(UsuarioPersonal usuarioPersonal)
        {
            dbSet.Update(usuarioPersonal);
            await SaveChangesAsync();
        }
    }
}
