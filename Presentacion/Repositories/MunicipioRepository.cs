using Modelos.ModelsDTO.Municipio;
using Presentacion.Repositories.IRepositories;
using System.Net.Http;

namespace Presentacion.Repositories
{
    public class MunicipioRepository : Repository<MunicipioResponseDTO>, IMunicipioRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public MunicipioRepository(HttpClient httpClient, string endpoint) : base(httpClient, endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }

        public async Task<IEnumerable<MunicipioResponseDTO>> GetByDepartamentoIdAsync(int departamentoId)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-departamento/{departamentoId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<MunicipioResponseDTO>>(content);
            }
            return null;
        }

        public async Task<MunicipioResponseDTO> GetByNombreAsync(string nombre)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-nombre/{nombre}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<MunicipioResponseDTO>(content);
            }
            return null;
        }
    }
}
