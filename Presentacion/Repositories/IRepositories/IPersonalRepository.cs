using Modelos.ModelsDTO.Personal;

namespace Presentacion.Repositories.IRepositories
{
    public interface IPersonalRepository : IRepository<PersonalResponseDTO>
    {
        Task<PersonalResponseDTO> GetByCedulaAsync(string cedula);
        Task<PersonalResponseDTO> GetByNumeroEmpleadoAsync(string numero);
        Task<IEnumerable<PersonalResponseDTO>> GetByCargoAsync(int cargoId);
        Task<IEnumerable<PersonalResponseDTO>> GetByDisciplinaAsync(int disciplinaId);
    }
}
