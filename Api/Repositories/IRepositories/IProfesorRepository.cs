using Modelos.Models;

namespace Api.Repositories.IRepositories;

public interface IProfesorRepository : IRepository<Profesor>
{
    Task<List<Profesor>> GetByAsignaturaAsync(string asignatura);
    Task<Profesor?> GetWithEstudiantesAsync(int profesorId);
    Task UpdateAsync(Profesor profesor);
}
