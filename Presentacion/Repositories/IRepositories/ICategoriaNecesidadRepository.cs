using Modelos.ModelsDTO.CategoriaNecesidad;

namespace Presentacion.Repositories.IRepositories
{
    public interface ICategoriaNecesidadRepository : IRepository<CategoriaNecesidadResponseDTO>
    {
        Task<CategoriaNecesidadResponseDTO> GetByNombreAsync(string nombre);
        Task<IEnumerable<CategoriaNecesidadResponseDTO>> GetWithSubcategoriasAsync();
    }
}
