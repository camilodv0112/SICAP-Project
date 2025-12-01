using Api.Data;
using Api.Repositories.IRepositories;
using Modelos.Models;

namespace Api.Repositories
{
    public class ResponsableEventoRepository : Repository<ResponsableEvento>, IResponsableEventoRepository
    {
        private readonly APIContext _db;

        public ResponsableEventoRepository(APIContext db) : base(db)
        {
            _db = db;
        }
    }
}
