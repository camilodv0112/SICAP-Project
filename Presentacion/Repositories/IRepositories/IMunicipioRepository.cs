using Modelos.ModelsDTO.Municipio;

namespace Presentacion.Repositories.IRepositories
{
    public interface IMunicipioRepository : IRepository<MunicipioResponseDTO>
    {
        Task<IEnumerable<MunicipioResponseDTO>> GetByDepartamentoIdAsync(int departamentoId);
        Task<MunicipioResponseDTO> GetByNombreAsync(string nombre);
    }
}
