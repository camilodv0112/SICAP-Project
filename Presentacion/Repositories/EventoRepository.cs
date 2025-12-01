using Modelos.ModelsDTO.Evento;
using Presentacion.Repositories.IRepositories;
using System.Net.Http;

namespace Presentacion.Repositories
{
    public class EventoRepository : Repository<EventoResponseDTO>, IEventoRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public EventoRepository(HttpClient httpClient, string endpoint) : base(httpClient, endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }

        public async Task<IEnumerable<EventoResponseDTO>> GetBySalonAsync(int salonId)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-salon/{salonId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<EventoResponseDTO>>(content);
            }
            return null;
        }

        public async Task<IEnumerable<EventoResponseDTO>> GetByFechaAsync(System.DateTime fecha)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-fecha/{fecha:yyyy-MM-dd}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<EventoResponseDTO>>(content);
            }
            return null;
        }

        public async Task<EventoResponseDTO> GetWithParticipantesAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/{id}/with-participantes");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<EventoResponseDTO>(content);
            }
            return null;
        }

        public async Task<EventoResponseDTO> GetWithResponsablesAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/{id}/with-responsables");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<EventoResponseDTO>(content);
            }
            return null;
        }
    }
}
