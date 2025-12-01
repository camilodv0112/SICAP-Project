using Modelos.Models;

namespace Api.Repositories.IRepositories;

public interface ICarreraRepository : IRepository<Carrera>
{
    Task<Carrera?> GetByNombreAsync(string nombre);
    Task<List<Carrera>> GetAllWithEstudiantesAsync();
    Task UpdateAsync(Carrera carrera);
}
