using Api.Data;
using Api.Repositories.IRepositories;
using Modelos.Models;

namespace Api.Repositories
{
    public class ParticipanteEventoRepository : Repository<ParticipanteEvento>, IParticipanteEventoRepository
    {
        private readonly APIContext _db;

        public ParticipanteEventoRepository(APIContext db) : base(db)
        {
            _db = db;
        }
    }
}
