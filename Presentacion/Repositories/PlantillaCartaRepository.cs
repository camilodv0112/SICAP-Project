using Modelos.ModelsDTO.PlantillaCarta;
using Presentacion.Repositories.IRepositories;
using System.Net.Http;

namespace Presentacion.Repositories
{
    public class PlantillaCartaRepository : Repository<PlantillaCartaResponseDTO>, IPlantillaCartaRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public PlantillaCartaRepository(HttpClient httpClient, string endpoint) : base(httpClient, endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }

        public async Task<PlantillaCartaResponseDTO> GetByNombreAsync(string nombre)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-nombre/{nombre}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<PlantillaCartaResponseDTO>(content);
            }
            return null;
        }
    }
}
