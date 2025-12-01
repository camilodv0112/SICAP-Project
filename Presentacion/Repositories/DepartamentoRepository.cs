using Modelos.ModelsDTO.Departamento;
using Presentacion.Repositories.IRepositories;
using System.Net.Http;

namespace Presentacion.Repositories
{
    public class DepartamentoRepository : Repository<DepartamentoResponseDTO>, IDepartamentoRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public DepartamentoRepository(HttpClient httpClient, string endpoint) : base(httpClient, endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }

        public async Task<DepartamentoResponseDTO> GetByNombreAsync(string nombre)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-nombre/{nombre}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<DepartamentoResponseDTO>(content);
            }
            return null;
        }

        public async Task<IEnumerable<DepartamentoResponseDTO>> GetWithMunicipiosAsync()
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/with-municipios");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<DepartamentoResponseDTO>>(content);
            }
            return null;
        }
    }
}
