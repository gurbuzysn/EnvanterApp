using EnvanterApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : class
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> models);
        bool Update(T model);
        bool Remove(T model);
        Task<bool> RemoveAsync(Guid id);
        bool RemoveRange(List<T> models);
        Task<int> SaveAsync();
    }
}
