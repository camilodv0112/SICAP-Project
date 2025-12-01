using Api.Data;
using Api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Modelos.Models;

namespace Api.Repositories
{
    public class SedeRepository : Repository<Sede>, ISedeRepository
    {
        private readonly APIContext _context;

        public SedeRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Sede>> GetByMunicipioAsync(int municipioId)
        {
            return await dbSet.Where(s => s.MunicipioID == municipioId).ToListAsync();
        }

        public async Task<List<Sede>> GetAllWithSalonesAsync()
        {
            return await dbSet.Include(s => s.Salones).ToListAsync();
        }

        public async Task UpdateAsync(Sede sede)
        {
            dbSet.Update(sede);
            await SaveChangesAsync();
        }
    }
}
