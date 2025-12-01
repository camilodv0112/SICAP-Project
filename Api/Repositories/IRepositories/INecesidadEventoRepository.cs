using Modelos.Models;

namespace Api.Repositories.IRepositories;

public interface INecesidadEventoRepository : IRepository<NecesidadEvento>
{
    Task<List<NecesidadEvento>> GetByEventoAsync(int eventoId);
    Task<List<NecesidadEvento>> GetBySubcategoriaAsync(int subcategoriaId);
    Task UpdateAsync(NecesidadEvento necesidad);
}
