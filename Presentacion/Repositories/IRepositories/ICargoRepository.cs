using Modelos.ModelsDTO.Cargo;

namespace Presentacion.Repositories.IRepositories
{
    public interface ICargoRepository : IRepository<CargoResponseDTO>
    {
        Task<CargoResponseDTO> GetByNombreAsync(string nombre);
    }
}
