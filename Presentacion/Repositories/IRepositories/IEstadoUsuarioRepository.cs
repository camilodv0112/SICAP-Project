using Modelos.ModelsDTO.EstadoUsuario;

namespace Presentacion.Repositories.IRepositories
{
    public interface IEstadoUsuarioRepository : IRepository<EstadoUsuarioResponseDTO>
    {
        Task<EstadoUsuarioResponseDTO> GetByNombreAsync(string nombre);
    }
}
