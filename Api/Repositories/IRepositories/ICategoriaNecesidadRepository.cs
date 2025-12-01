using Modelos.Models;

namespace Api.Repositories.IRepositories;

public interface ICategoriaNecesidadRepository : IRepository<CategoriaNecesidad>
{
    Task<CategoriaNecesidad?> GetByNombreAsync(string nombre);
    Task<List<CategoriaNecesidad>> GetAllWithSubcategoriasAsync();
    Task UpdateAsync(CategoriaNecesidad categoria);
}
