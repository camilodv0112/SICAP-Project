using Modelos.ModelsDTO.Personal;
using Presentacion.Repositories.IRepositories;
using System.Net.Http;

namespace Presentacion.Repositories
{
    public class PersonalRepository : Repository<PersonalResponseDTO>, IPersonalRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public PersonalRepository(HttpClient httpClient, string endpoint) : base(httpClient, endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }

        public async Task<PersonalResponseDTO> GetByCedulaAsync(string cedula)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-cedula/{cedula}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<PersonalResponseDTO>(content);
            }
            return null;
        }

        public async Task<PersonalResponseDTO> GetByNumeroEmpleadoAsync(string numero)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-numero-empleado/{numero}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<PersonalResponseDTO>(content);
            }
            return null;
        }

        public async Task<IEnumerable<PersonalResponseDTO>> GetByCargoAsync(int cargoId)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-cargo/{cargoId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<PersonalResponseDTO>>(content);
            }
            return null;
        }

        public async Task<IEnumerable<PersonalResponseDTO>> GetByDisciplinaAsync(int disciplinaId)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-disciplina/{disciplinaId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<PersonalResponseDTO>>(content);
            }
            return null;
        }
    }
}
