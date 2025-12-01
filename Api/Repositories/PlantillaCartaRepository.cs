using Api.Data;
using Api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Modelos.Models;

namespace Api.Repositories
{
    public class PlantillaCartaRepository : Repository<PlantillaCarta>, IPlantillaCartaRepository
    {
        private readonly APIContext _context;

        public PlantillaCartaRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PlantillaCarta?> GetByNombreAsync(string nombre)
        {
            return await dbSet.FirstOrDefaultAsync(p => p.Nombre == nombre);
        }

        public async Task UpdateAsync(PlantillaCarta plantilla)
        {
            dbSet.Update(plantilla);
            await SaveChangesAsync();
        }
    }
}
