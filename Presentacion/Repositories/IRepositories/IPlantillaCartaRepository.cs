using Modelos.ModelsDTO.PlantillaCarta;

namespace Presentacion.Repositories.IRepositories
{
    public interface IPlantillaCartaRepository : IRepository<PlantillaCartaResponseDTO>
    {
        Task<PlantillaCartaResponseDTO> GetByNombreAsync(string nombre);
    }
}
