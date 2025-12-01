using Modelos.Models;

namespace Api.Repositories.IRepositories;

public interface IEstadoUsuarioRepository : IRepository<EstadoUsuario>
{
    Task<EstadoUsuario?> GetByNombreAsync(string nombre);
    Task UpdateAsync(EstadoUsuario estado);
}
