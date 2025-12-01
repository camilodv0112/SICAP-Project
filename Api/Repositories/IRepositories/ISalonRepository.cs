using Modelos.Models;

namespace Api.Repositories.IRepositories;

public interface ISalonRepository : IRepository<Salon>
{
    Task<List<Salon>> GetBySedeAsync(int sedeId);
    Task<List<Salon>> GetByCapacidadMinimaAsync(int capacidadMinima);
    Task UpdateAsync(Salon salon);
}
