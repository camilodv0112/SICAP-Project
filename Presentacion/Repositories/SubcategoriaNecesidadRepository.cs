using Modelos.ModelsDTO.SubcategoriaNecesidad;
using Presentacion.Repositories.IRepositories;
using System.Net.Http;

namespace Presentacion.Repositories
{
    public class SubcategoriaNecesidadRepository : Repository<SubcategoriaNecesidadResponseDTO>, ISubcategoriaNecesidadRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public SubcategoriaNecesidadRepository(HttpClient httpClient, string endpoint) : base(httpClient, endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }

        public async Task<IEnumerable<SubcategoriaNecesidadResponseDTO>> GetByCategoriaIdAsync(int categoriaId)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-categoria/{categoriaId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<SubcategoriaNecesidadResponseDTO>>(content);
            }
            return null;
        }
    }
}
