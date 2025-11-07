using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHomeMonitor.Services
{
    public interface IRepository<T> where T : new()
    {
        Task InitializeAsync();
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<int> InsertAsync(T item);
        Task<int> UpdateAsync(T item);
        Task<int> DeleteAsync(T item);
        Task<int> DeleteAllAsync();
    }
}
