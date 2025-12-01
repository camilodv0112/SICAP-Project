using Modelos.ModelsDTO.Cargo;
using Presentacion.Repositories.IRepositories;
using System.Net.Http;

namespace Presentacion.Repositories
{
    public class CargoRepository : Repository<CargoResponseDTO>, ICargoRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public CargoRepository(HttpClient httpClient, string endpoint) : base(httpClient, endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }

        public async Task<CargoResponseDTO> GetByNombreAsync(string nombre)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-nombre/{nombre}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<CargoResponseDTO>(content);
            }
            return null;
        }
    }
}
