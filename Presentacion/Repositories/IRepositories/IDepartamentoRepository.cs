using Modelos.ModelsDTO.Departamento;

namespace Presentacion.Repositories.IRepositories
{
    public interface IDepartamentoRepository : IRepository<DepartamentoResponseDTO>
    {
        Task<DepartamentoResponseDTO> GetByNombreAsync(string nombre);
        Task<IEnumerable<DepartamentoResponseDTO>> GetWithMunicipiosAsync();
    }
}
