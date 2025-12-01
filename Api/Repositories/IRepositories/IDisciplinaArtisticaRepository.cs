using Modelos.Models;

namespace Api.Repositories.IRepositories;

public interface IDisciplinaArtisticaRepository : IRepository<DisciplinaArtistica>
{
    Task<DisciplinaArtistica?> GetByNombreAsync(string nombre);
    Task UpdateAsync(DisciplinaArtistica disciplina);
}
