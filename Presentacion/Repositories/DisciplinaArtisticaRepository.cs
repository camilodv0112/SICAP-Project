using Modelos.ModelsDTO.DisciplinaArtistica;
using Presentacion.Repositories.IRepositories;
using System.Net.Http;

namespace Presentacion.Repositories
{
    public class DisciplinaArtisticaRepository : Repository<DisciplinaArtisticaResponseDTO>, IDisciplinaArtisticaRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public DisciplinaArtisticaRepository(HttpClient httpClient, string endpoint) : base(httpClient, endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }

        public async Task<DisciplinaArtisticaResponseDTO> GetByNombreAsync(string nombre)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-nombre/{nombre}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<DisciplinaArtisticaResponseDTO>(content);
            }
            return null;
        }
    }
}
