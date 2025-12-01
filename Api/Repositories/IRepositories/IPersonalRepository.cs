using Modelos.Models;

namespace Api.Repositories.IRepositories;

public interface IPersonalRepository : IRepository<Personal>
{
    Task<Personal?> GetByCedulaAsync(string cedula);
    Task<Personal?> GetByNumeroEmpleadoAsync(string numeroEmpleado);
    Task<List<Personal>> GetByCargoAsync(int cargoId);
    Task<List<Personal>> GetByDisciplinaAsync(int disciplinaId);
    Task UpdateAsync(Personal personal);
}
