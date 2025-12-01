using Modelos.ModelsDTO.Salon;
using Presentacion.Repositories.IRepositories;
using System.Net.Http;

namespace Presentacion.Repositories
{
    public class SalonRepository : Repository<SalonResponseDTO>, ISalonRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public SalonRepository(HttpClient httpClient, string endpoint) : base(httpClient, endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }

        public async Task<IEnumerable<SalonResponseDTO>> GetBySedeIdAsync(int sedeId)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-sede/{sedeId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<SalonResponseDTO>>(content);
            }
            return null;
        }

        public async Task<IEnumerable<SalonResponseDTO>> GetByCapacidadMinimaAsync(int capacidad)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-capacidad-minima/{capacidad}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<SalonResponseDTO>>(content);
            }
            return null;
        }
    }
}
