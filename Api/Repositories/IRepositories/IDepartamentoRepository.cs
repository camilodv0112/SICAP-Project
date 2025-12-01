using Modelos.Models;

namespace Api.Repositories.IRepositories;

public interface IDepartamentoRepository : IRepository<Departamento>
{
    Task<Departamento?> GetByNombreAsync(string nombre);
    Task<List<Departamento>> GetAllWithMunicipiosAsync();
    Task UpdateAsync(Departamento departamento);
}
