using Modelos.ModelsDTO.ResponsableEvento;
using Presentacion.Repositories.IRepositories;
using System.Net.Http;

namespace Presentacion.Repositories
{
    public class ResponsableEventoRepository : Repository<ResponsableEventoResponseDTO>, IResponsableEventoRepository
    {
        public ResponsableEventoRepository(HttpClient httpClient, string endpoint) : base(httpClient, endpoint)
        {
        }
    }
}
