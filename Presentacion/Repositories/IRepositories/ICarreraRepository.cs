using Modelos.ModelsDTO.Carrera;

namespace Presentacion.Repositories.IRepositories
{
    public interface ICarreraRepository : IRepository<CarreraResponseDTO>
    {
        Task<CarreraResponseDTO> GetByNombreAsync(string nombre);
        Task<IEnumerable<CarreraResponseDTO>> GetWithEstudiantesAsync();
    }
}
