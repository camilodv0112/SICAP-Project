using Modelos.ModelsDTO.Salon;

namespace Presentacion.Repositories.IRepositories
{
    public interface ISalonRepository : IRepository<SalonResponseDTO>
    {
        Task<IEnumerable<SalonResponseDTO>> GetBySedeIdAsync(int sedeId);
        Task<IEnumerable<SalonResponseDTO>> GetByCapacidadMinimaAsync(int capacidad);
    }
}
