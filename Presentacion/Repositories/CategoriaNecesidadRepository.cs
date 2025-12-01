using Modelos.ModelsDTO.CategoriaNecesidad;
using Presentacion.Repositories.IRepositories;
using System.Net.Http;

namespace Presentacion.Repositories
{
    public class CategoriaNecesidadRepository : Repository<CategoriaNecesidadResponseDTO>, ICategoriaNecesidadRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public CategoriaNecesidadRepository(HttpClient httpClient, string endpoint) : base(httpClient, endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }

        public async Task<CategoriaNecesidadResponseDTO> GetByNombreAsync(string nombre)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-nombre/{nombre}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<CategoriaNecesidadResponseDTO>(content);
            }
            return null;
        }

        public async Task<IEnumerable<CategoriaNecesidadResponseDTO>> GetWithSubcategoriasAsync()
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/with-subcategorias");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<CategoriaNecesidadResponseDTO>>(content);
            }
            return null;
        }
    }
}
