using Modelos.ModelsDTO.CartaGenerada;

namespace Presentacion.Repositories.IRepositories
{
    public interface ICartaGeneradaRepository : IRepository<CartaGeneradaResponseDTO>
    {
        Task<IEnumerable<CartaGeneradaResponseDTO>> GetByPlantillaIdAsync(int plantillaId);
        Task<IEnumerable<CartaGeneradaResponseDTO>> GetByUsuarioIdAsync(int usuarioId);
        Task<IEnumerable<CartaGeneradaResponseDTO>> GetByFechaAsync(System.DateTime fecha);
    }
}
