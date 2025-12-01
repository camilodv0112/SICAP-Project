using Modelos.ModelsDTO.Profesor;
using Presentacion.Repositories.IRepositories;
using System.Net.Http;

namespace Presentacion.Repositories
{
    public class ProfesorRepository : Repository<ProfesorResponseDTO>, IProfesorRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public ProfesorRepository(HttpClient httpClient, string endpoint) : base(httpClient, endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }

        public async Task<IEnumerable<ProfesorResponseDTO>> GetByAsignaturaAsync(string asignatura)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-asignatura/{asignatura}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<ProfesorResponseDTO>>(content);
            }
            return null;
        }

        public async Task<ProfesorResponseDTO> GetWithEstudiantesAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/{id}/with-estudiantes");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<ProfesorResponseDTO>(content);
            }
            return null;
        }
    }
}
