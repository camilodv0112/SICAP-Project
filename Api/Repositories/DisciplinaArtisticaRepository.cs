using Api.Data;
using Api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Modelos.Models;

namespace Api.Repositories
{
    public class DisciplinaArtisticaRepository : Repository<DisciplinaArtistica>, IDisciplinaArtisticaRepository
    {
        private readonly APIContext _context;

        public DisciplinaArtisticaRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DisciplinaArtistica?> GetByNombreAsync(string nombre)
        {
            return await dbSet.FirstOrDefaultAsync(d => d.Nombre == nombre);
        }

        public async Task UpdateAsync(DisciplinaArtistica disciplina)
        {
            dbSet.Update(disciplina);
            await SaveChangesAsync();
        }
    }
}
