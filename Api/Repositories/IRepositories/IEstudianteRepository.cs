using Modelos.Models;

namespace Api.Repositories.IRepositories;

public interface IEstudianteRepository : IRepository<Estudiante>
{
    Task<Estudiante?> GetByCedulaAsync(string cedula);
    Task<Estudiante?> GetByCarnetAsync(string carnet);
    Task<List<Estudiante>> GetByCarreraAsync(int carreraId);
    Task<List<Estudiante>> GetByDisciplinaAsync(int disciplinaId);
    Task<Estudiante?> GetWithProfesoresAsync(int estudianteId);
    Task UpdateAsync(Estudiante estudiante);
}
