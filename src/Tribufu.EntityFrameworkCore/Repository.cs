// Copyright (c) Tribufu. All Rights Reserved.
// SPDX-License-Identifier: MIT

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tribufu.EntityFrameworkCore
{
    public class Repository<C, T, K> : IRepository<T, K> where C : DbContext where T : class
    {
        protected readonly C _dbContext;

        protected readonly DbSet<T> _dbSet;

        public Repository(C dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = dbContext.Set<T>();
        }

        public virtual void Seed()
        {
        }

        public virtual async Task SeedAsync()
        {
        }

        public virtual IList<T> List()
        {
            return [.. _dbSet];
        }

        public virtual async Task<IList<T>> ListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual IList<T> List(uint page, uint limit)
        {
            return _dbSet.Skip((int)((page < 1 ? 0 : page - 1) * limit)).Take((int)limit).ToList();
        }

        public virtual async Task<IList<T>> ListAsync(uint page, uint limit)
        {
            return await _dbSet.Skip((int)((page < 1 ? 0 : page - 1) * limit)).Take((int)limit).ToListAsync();
        }

        public virtual T? Find(K key)
        {
            return _dbSet.Find(key);
        }

        public virtual async Task<T?> FindAsync(K key)
        {
            return await _dbSet.FindAsync(key);
        }

        public virtual T? Create(T entity)
        {
            _dbSet.Add(entity);
            var result = _dbContext.SaveChanges();
            return result > 0 ? entity : null;
        }

        public virtual async Task<T?> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0 ? entity : null;
        }

        public virtual T? Update(T entity)
        {
            _dbSet.Update(entity);
            var result = _dbContext.SaveChanges();
            return result > 0 ? entity : null;
        }

        public virtual async Task<T?> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0 ? entity : null;
        }

        public virtual void Delete(K key)
        {
            var entity = _dbSet.Find(key);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public virtual async Task DeleteAsync(K key)
        {
            var entity = await _dbSet.FindAsync(key);
            if (entity != null)
            {
                await DeleteAsync(entity);
            }
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
