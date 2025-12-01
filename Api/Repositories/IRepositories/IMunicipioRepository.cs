using Modelos.Models;

namespace Api.Repositories.IRepositories;

public interface IMunicipioRepository : IRepository<Municipio>
{
    Task<List<Municipio>> GetByDepartamentoAsync(int departamentoId);
    Task<Municipio?> GetByNombreAsync(string nombre);
    Task UpdateAsync(Municipio municipio);
}
