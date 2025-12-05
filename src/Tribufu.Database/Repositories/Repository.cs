// Copyright (c) Tribufu. All Rights Reserved.
// SPDX-License-Identifier: MIT

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tribufu.Database.Repositories
{
    public class Repository<C, T, K> : IRepository<T, K> where C : DbContext where T : class
    {
        protected readonly C _context;

        protected readonly DbSet<T> _dbSet;

        public Repository(C context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<T>();
        }

        public virtual async Task SeedAsync()
        {
        }

        public virtual IList<T> GetAll()
        {
            return [.. _dbSet];
        }

        public virtual async Task<IList<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual IList<T> GetPage(uint page, uint limit)
        {
            return _dbSet.Skip((int)((page < 1 ? 0 : page - 1) * limit)).Take((int)limit).ToList();
        }

        public virtual async Task<IList<T>> GetPageAsync(uint page, uint limit)
        {
            return await _dbSet.Skip((int)((page < 1 ? 0 : page - 1) * limit)).Take((int)limit).ToListAsync();
        }

        public virtual T? GetOne(K key)
        {
            return _dbSet.Find(key);
        }

        public virtual async Task<T?> GetOneAsync(K key)
        {
            return await _dbSet.FindAsync(key);
        }

        public virtual T? Create(T entity)
        {
            _dbSet.Add(entity);

            var result = _context.SaveChanges();
            return result > 0 ? entity : null;
        }

        public virtual async Task<T?> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0 ? entity : null;
        }

        public virtual T? Update(T entity)
        {
            _dbSet.Update(entity);
            var result = _context.SaveChanges();
            return result > 0 ? entity : null;
        }

        public virtual async Task<T?> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            var result = await _context.SaveChangesAsync();
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
            _context.SaveChanges();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
