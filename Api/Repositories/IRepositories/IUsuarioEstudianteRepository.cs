using Modelos.Models;

namespace Api.Repositories.IRepositories;

public interface IUsuarioEstudianteRepository : IRepository<UsuarioEstudiante>
{
    Task<UsuarioEstudiante?> GetByUsuarioAsync(string usuario);
    Task<List<UsuarioEstudiante>> GetByEstudianteIdAsync(int estudianteId);
    Task<List<UsuarioEstudiante>> GetByEstadoAsync(int estadoId);
    Task UpdateAsync(UsuarioEstudiante usuarioEstudiante);
}
