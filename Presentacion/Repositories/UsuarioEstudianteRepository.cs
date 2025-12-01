using Modelos.ModelsDTO.UsuarioEstudiante;
using Presentacion.Repositories.IRepositories;
using System.Net.Http;

namespace Presentacion.Repositories
{
    public class UsuarioEstudianteRepository : Repository<UsuarioEstudianteResponseDTO>, IUsuarioEstudianteRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public UsuarioEstudianteRepository(HttpClient httpClient, string endpoint) : base(httpClient, endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }

        public async Task<UsuarioEstudianteResponseDTO> GetByUsuarioAsync(string usuario)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-usuario/{usuario}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<UsuarioEstudianteResponseDTO>(content);
            }
            return null;
        }

        public async Task<IEnumerable<UsuarioEstudianteResponseDTO>> GetByEstudianteIdAsync(int estudianteId)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-estudiante/{estudianteId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<UsuarioEstudianteResponseDTO>>(content);
            }
            return null;
        }

        public async Task<IEnumerable<UsuarioEstudianteResponseDTO>> GetByEstadoIdAsync(int estadoId)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-estado/{estadoId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<UsuarioEstudianteResponseDTO>>(content);
            }
            return null;
        }
    }
}
