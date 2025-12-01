using Modelos.ModelsDTO.Sede;

namespace Presentacion.Repositories.IRepositories
{
    public interface ISedeRepository : IRepository<SedeResponseDTO>
    {
        Task<IEnumerable<SedeResponseDTO>> GetByMunicipioIdAsync(int municipioId);
        Task<IEnumerable<SedeResponseDTO>> GetWithSalonesAsync();
    }
}
