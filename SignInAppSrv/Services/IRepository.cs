using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SignInAppSrv.Services
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByGroupId(int id);
        Task<T> CreateAsync(T item);
        Task<T> UpdateAsync(int id, T item);
        Task<bool> DeleteAsync(int id);
    }
}
