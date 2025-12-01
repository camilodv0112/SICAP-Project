using Api.Data;
using Api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Modelos.Models;

namespace Api.Repositories
{
    public class EstadoUsuarioRepository : Repository<EstadoUsuario>, IEstadoUsuarioRepository
    {
        private readonly APIContext _context;

        public EstadoUsuarioRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        public async Task<EstadoUsuario?> GetByNombreAsync(string nombre)
        {
            return await dbSet.FirstOrDefaultAsync(e => e.Nombre == nombre);
        }

        public async Task UpdateAsync(EstadoUsuario estado)
        {
            dbSet.Update(estado);
            await SaveChangesAsync();
        }
    }
}
