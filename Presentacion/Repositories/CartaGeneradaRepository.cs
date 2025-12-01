using Modelos.ModelsDTO.CartaGenerada;
using Presentacion.Repositories.IRepositories;
using System.Net.Http;

namespace Presentacion.Repositories
{
    public class CartaGeneradaRepository : Repository<CartaGeneradaResponseDTO>, ICartaGeneradaRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public CartaGeneradaRepository(HttpClient httpClient, string endpoint) : base(httpClient, endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }

        public async Task<IEnumerable<CartaGeneradaResponseDTO>> GetByPlantillaIdAsync(int plantillaId)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-plantilla/{plantillaId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<CartaGeneradaResponseDTO>>(content);
            }
            return null;
        }

        public async Task<IEnumerable<CartaGeneradaResponseDTO>> GetByUsuarioIdAsync(int usuarioId)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-usuario/{usuarioId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<CartaGeneradaResponseDTO>>(content);
            }
            return null;
        }

        public async Task<IEnumerable<CartaGeneradaResponseDTO>> GetByFechaAsync(System.DateTime fecha)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-fecha/{fecha:yyyy-MM-dd}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<CartaGeneradaResponseDTO>>(content);
            }
            return null;
        }
    }
}
