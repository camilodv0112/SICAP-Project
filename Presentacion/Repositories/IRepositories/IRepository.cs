using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentacion.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(object dto);
        Task<bool> UpdateAsync(int id, object dto);
        Task<bool> DeleteAsync(int id);
    }
}
