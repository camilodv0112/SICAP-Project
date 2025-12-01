using Modelos.ModelsDTO.NecesidadEvento;

namespace Presentacion.Repositories.IRepositories
{
    public interface INecesidadEventoRepository : IRepository<NecesidadEventoResponseDTO>
    {
        Task<IEnumerable<NecesidadEventoResponseDTO>> GetByEventoIdAsync(int eventoId);
        Task<IEnumerable<NecesidadEventoResponseDTO>> GetBySubcategoriaIdAsync(int subcategoriaId);
    }
}
