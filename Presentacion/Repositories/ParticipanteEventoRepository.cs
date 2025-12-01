using Modelos.ModelsDTO.ParticipanteEvento;
using Presentacion.Repositories.IRepositories;
using System.Net.Http;

namespace Presentacion.Repositories
{
    public class ParticipanteEventoRepository : Repository<ParticipanteEventoResponseDTO>, IParticipanteEventoRepository
    {
        public ParticipanteEventoRepository(HttpClient httpClient, string endpoint) : base(httpClient, endpoint)
        {
        }
    }
}
