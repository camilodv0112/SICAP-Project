using Modelos.Models;

namespace Api.Repositories.IRepositories;

public interface ICargoRepository : IRepository<Cargo>
{
    Task<Cargo?> GetByNombreAsync(string nombre);
    Task UpdateAsync(Cargo cargo);
}
