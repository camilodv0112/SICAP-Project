using Modelos.ModelsDTO.SubcategoriaNecesidad;

namespace Presentacion.Repositories.IRepositories
{
    public interface ISubcategoriaNecesidadRepository : IRepository<SubcategoriaNecesidadResponseDTO>
    {
        Task<IEnumerable<SubcategoriaNecesidadResponseDTO>> GetByCategoriaIdAsync(int categoriaId);
    }
}
