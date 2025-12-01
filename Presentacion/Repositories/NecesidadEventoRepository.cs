using Modelos.ModelsDTO.NecesidadEvento;
using Presentacion.Repositories.IRepositories;
using System.Net.Http;

namespace Presentacion.Repositories
{
    public class NecesidadEventoRepository : Repository<NecesidadEventoResponseDTO>, INecesidadEventoRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public NecesidadEventoRepository(HttpClient httpClient, string endpoint) : base(httpClient, endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }

        public async Task<IEnumerable<NecesidadEventoResponseDTO>> GetByEventoIdAsync(int eventoId)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-evento/{eventoId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<NecesidadEventoResponseDTO>>(content);
            }
            return null;
        }

        public async Task<IEnumerable<NecesidadEventoResponseDTO>> GetBySubcategoriaIdAsync(int subcategoriaId)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-subcategoria/{subcategoriaId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<NecesidadEventoResponseDTO>>(content);
            }
            return null;
        }
    }
}
