using Modelos.ModelsDTO.EstadoUsuario;
using Presentacion.Repositories.IRepositories;
using System.Net.Http;

namespace Presentacion.Repositories
{
    public class EstadoUsuarioRepository : Repository<EstadoUsuarioResponseDTO>, IEstadoUsuarioRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public EstadoUsuarioRepository(HttpClient httpClient, string endpoint) : base(httpClient, endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }

        public async Task<EstadoUsuarioResponseDTO> GetByNombreAsync(string nombre)
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/by-nombre/{nombre}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<EstadoUsuarioResponseDTO>(content);
            }
            return null;
        }
    }
}
