using Modelos.Models;

namespace Api.Repositories.IRepositories;

public interface ICartaGeneradaRepository : IRepository<CartaGenerada>
{
    Task<List<CartaGenerada>> GetByPlantillaAsync(int plantillaId);
    Task<List<CartaGenerada>> GetByUsuarioAsync(int usuarioPersonalId);
    Task<List<CartaGenerada>> GetByFechaAsync(DateTime fecha);
    Task UpdateAsync(CartaGenerada carta);
}
