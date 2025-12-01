using Modelos.Models;

namespace Api.Repositories.IRepositories;

public interface ISubcategoriaNecesidadRepository : IRepository<SubcategoriaNecesidad>
{
    Task<List<SubcategoriaNecesidad>> GetByCategoriaAsync(int categoriaId);
    Task UpdateAsync(SubcategoriaNecesidad subcategoria);
}
