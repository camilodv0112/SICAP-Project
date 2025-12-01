using Modelos.ModelsDTO.Profesor;

namespace Presentacion.Repositories.IRepositories
{
    public interface IProfesorRepository : IRepository<ProfesorResponseDTO>
    {
        Task<IEnumerable<ProfesorResponseDTO>> GetByAsignaturaAsync(string asignatura);
        Task<ProfesorResponseDTO> GetWithEstudiantesAsync(int id);
    }
}
