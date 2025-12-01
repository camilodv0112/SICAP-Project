using Modelos.ModelsDTO.DisciplinaArtistica;

namespace Presentacion.Repositories.IRepositories
{
    public interface IDisciplinaArtisticaRepository : IRepository<DisciplinaArtisticaResponseDTO>
    {
        Task<DisciplinaArtisticaResponseDTO> GetByNombreAsync(string nombre);
    }
}
