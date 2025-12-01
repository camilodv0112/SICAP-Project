using Modelos.ModelsDTO.Estudiante;
using Presentacion.Repositories.IRepositories;
using System.Net.Http;

namespace Presentacion.Repositories
{
    public class EstudianteRepository : Repository<EstudianteResponseDTO>, IEstudianteRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public EstudianteRepository(HttpClient httpClient, string endpoint) : base(httpClient, endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }

        public async Task<EstudianteResponseDTO> GetByCedulaAsync(string cedula)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-cedula/{cedula}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<EstudianteResponseDTO>(content);
            }
            return null;
        }

        public async Task<EstudianteResponseDTO> GetByCarnetAsync(string carnet)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-carnet/{carnet}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<EstudianteResponseDTO>(content);
            }
            return null;
        }

        public async Task<IEnumerable<EstudianteResponseDTO>> GetByCarreraAsync(int carreraId)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-carrera/{carreraId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<EstudianteResponseDTO>>(content);
            }
            return null;
        }

        public async Task<IEnumerable<EstudianteResponseDTO>> GetByDisciplinaAsync(int disciplinaId)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-disciplina/{disciplinaId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<EstudianteResponseDTO>>(content);
            }
            return null;
        }

        public async Task<EstudianteResponseDTO> GetWithProfesoresAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/{id}/with-profesores");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<EstudianteResponseDTO>(content);
            }
            return null;
        }
    }
}
