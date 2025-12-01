using Modelos.Models;

namespace Api.Repositories.IRepositories;

public interface IUsuarioPersonalRepository : IRepository<UsuarioPersonal>
{
    Task<UsuarioPersonal?> GetByUsuarioAsync(string usuario);
    Task<List<UsuarioPersonal>> GetByPersonalIdAsync(int personalId);
    Task<List<UsuarioPersonal>> GetByEstadoAsync(int estadoId);
    Task UpdateAsync(UsuarioPersonal usuarioPersonal);
}
