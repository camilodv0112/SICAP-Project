using Modelos.ModelsDTO.UsuarioEstudiante;

namespace Presentacion.Repositories.IRepositories
{
    public interface IUsuarioEstudianteRepository : IRepository<UsuarioEstudianteResponseDTO>
    {
        Task<UsuarioEstudianteResponseDTO> GetByUsuarioAsync(string usuario);
        Task<IEnumerable<UsuarioEstudianteResponseDTO>> GetByEstudianteIdAsync(int estudianteId);
        Task<IEnumerable<UsuarioEstudianteResponseDTO>> GetByEstadoIdAsync(int estadoId);
    }
}
