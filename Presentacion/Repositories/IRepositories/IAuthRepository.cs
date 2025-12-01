using System.Threading.Tasks;

namespace Presentacion.Repositories.IRepositories
{
    public interface IAuthRepository
    {
        Task<string> LoginPersonalAsync(string usuario, string password);
        Task<string> LoginEstudianteAsync(string usuario, string password);
    }
}
