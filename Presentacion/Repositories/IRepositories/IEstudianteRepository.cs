using Modelos.ModelsDTO.Estudiante;

namespace Presentacion.Repositories.IRepositories
{
    public interface IEstudianteRepository : IRepository<EstudianteResponseDTO>
    {
        Task<EstudianteResponseDTO> GetByCedulaAsync(string cedula);
        Task<EstudianteResponseDTO> GetByCarnetAsync(string carnet);
        Task<IEnumerable<EstudianteResponseDTO>> GetByCarreraAsync(int carreraId);
        Task<IEnumerable<EstudianteResponseDTO>> GetByDisciplinaAsync(int disciplinaId);
        Task<EstudianteResponseDTO> GetWithProfesoresAsync(int id);
    }
}
