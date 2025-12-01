using Modelos.Models;

namespace Api.Repositories.IRepositories;

public interface ISedeRepository : IRepository<Sede>
{
    Task<List<Sede>> GetByMunicipioAsync(int municipioId);
    Task<List<Sede>> GetAllWithSalonesAsync();
    Task UpdateAsync(Sede sede);
}
