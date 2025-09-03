﻿using EnvanterApp.Application.Repositories;
using EnvanterApp.Domain.Entities;
using EnvanterApp.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly EnvanterAppDbContext _context;

        public WriteRepository(EnvanterAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(T model)
        {
            EntityEntry<T> entityEntry = await _context.AddAsync(model);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> models)
        {
            await _context.Set<T>().AddRangeAsync(models);
            return true;
        }


        public async Task<bool> RemoveAsync(Guid id)
        {
            T model = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            return Remove(model);
        }

        public bool Remove(T model)
        {
            EntityEntry<T> entityEntry = _context.Remove(model);
            return entityEntry.State == EntityState.Deleted;
        }

        public bool RemoveRange(List<T> models)
        {
            _context.Set<T>().RemoveRange(models);
            return true;
        }

        public bool Update(T model)
        {
            EntityEntry entityEntry = _context.Update(model);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
    }
}
