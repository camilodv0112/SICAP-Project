using Modelos.ModelsDTO.Sede;
using Presentacion.Repositories.IRepositories;
using System.Net.Http;

namespace Presentacion.Repositories
{
    public class SedeRepository : Repository<SedeResponseDTO>, ISedeRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public SedeRepository(HttpClient httpClient, string endpoint) : base(httpClient, endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }

        public async Task<IEnumerable<SedeResponseDTO>> GetByMunicipioIdAsync(int municipioId)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-municipio/{municipioId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<SedeResponseDTO>>(content);
            }
            return null;
        }

        public async Task<IEnumerable<SedeResponseDTO>> GetWithSalonesAsync()
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/with-salones");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<SedeResponseDTO>>(content);
            }
            return null;
        }
    }
}
