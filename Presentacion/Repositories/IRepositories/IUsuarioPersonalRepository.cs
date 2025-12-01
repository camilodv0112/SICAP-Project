using Modelos.ModelsDTO.UsuarioPersonal;

namespace Presentacion.Repositories.IRepositories
{
    public interface IUsuarioPersonalRepository : IRepository<UsuarioPersonalResponseDTO>
    {
        Task<UsuarioPersonalResponseDTO> GetByUsuarioAsync(string usuario);
        Task<IEnumerable<UsuarioPersonalResponseDTO>> GetByPersonalIdAsync(int personalId);
        Task<IEnumerable<UsuarioPersonalResponseDTO>> GetByEstadoIdAsync(int estadoId);
    }
}
