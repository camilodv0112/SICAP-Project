using Modelos.ModelsDTO.Carrera;
using Presentacion.Repositories.IRepositories;
using System.Net.Http;

namespace Presentacion.Repositories
{
    public class CarreraRepository : Repository<CarreraResponseDTO>, ICarreraRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public CarreraRepository(HttpClient httpClient, string endpoint) : base(httpClient, endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }

        public async Task<CarreraResponseDTO> GetByNombreAsync(string nombre)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-nombre/{nombre}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<CarreraResponseDTO>(content);
            }
            return null;
        }

        public async Task<IEnumerable<CarreraResponseDTO>> GetWithEstudiantesAsync()
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/with-estudiantes");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<CarreraResponseDTO>>(content);
            }
            return null;
        }
    }
}
