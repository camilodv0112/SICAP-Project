using Modelos.Models;

namespace Api.Repositories.IRepositories;

public interface IPlantillaCartaRepository : IRepository<PlantillaCarta>
{
    Task<PlantillaCarta?> GetByNombreAsync(string nombre);
    Task UpdateAsync(PlantillaCarta plantilla);
}
