using Modelos.ModelsDTO.UsuarioPersonal;
using Presentacion.Repositories.IRepositories;
using System.Net.Http;

namespace Presentacion.Repositories
{
    public class UsuarioPersonalRepository : Repository<UsuarioPersonalResponseDTO>, IUsuarioPersonalRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public UsuarioPersonalRepository(HttpClient httpClient, string endpoint) : base(httpClient, endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }

        public async Task<UsuarioPersonalResponseDTO> GetByUsuarioAsync(string usuario)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-usuario/{usuario}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<UsuarioPersonalResponseDTO>(content);
            }
            return null;
        }

        public async Task<IEnumerable<UsuarioPersonalResponseDTO>> GetByPersonalIdAsync(int personalId)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-personal/{personalId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<UsuarioPersonalResponseDTO>>(content);
            }
            return null;
        }

        public async Task<IEnumerable<UsuarioPersonalResponseDTO>> GetByEstadoIdAsync(int estadoId)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-estado/{estadoId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<UsuarioPersonalResponseDTO>>(content);
            }
            return null;
        }
    }
}
