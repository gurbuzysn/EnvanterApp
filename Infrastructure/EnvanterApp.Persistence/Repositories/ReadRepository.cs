using EnvanterApp.Application.Repositories;
using EnvanterApp.Domain.Entities;
using EnvanterApp.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EnvanterApp.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : class
    {
        private readonly EnvanterAppDbContext _context;

        public ReadRepository(EnvanterAppDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll() => _context.Set<T>();
        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method) => _context.Set<T>().Where(method);
        //public async Task<T> GetByIdAsync(Guid id) => await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method) => await _context.Set<T>().FirstOrDefaultAsync(method);
    }
}
