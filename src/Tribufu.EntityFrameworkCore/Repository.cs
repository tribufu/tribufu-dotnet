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
            return Paginate(_dbSet, page, limit).ToList();
        }

        public virtual async Task<IList<T>> ListAsync(uint page, uint limit)
        {
            return await Paginate(_dbSet, page, limit).ToListAsync();
        }

        public virtual bool Exists(K key)
        {
            return _dbSet.Find(key) != null;
        }

        public virtual async Task<bool> ExistsAsync(K key)
        {
            return await _dbSet.FindAsync(key) != null;
        }

        public virtual T Find(K key)
        {
            return _dbSet.Find(key) ?? throw EntityNotFoundException.For<T>(key);
        }

        public virtual async Task<T> FindAsync(K key)
        {
            return await _dbSet.FindAsync(key) ?? throw EntityNotFoundException.For<T>(key);
        }

        public virtual T Create(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual T Update(T entity)
        {
            _dbSet.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual void Delete(K key)
        {
            Delete(Find(key));
        }

        public virtual async Task DeleteAsync(K key)
        {
            await DeleteAsync(await FindAsync(key));
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

        /// <summary>
        /// Skip to a one-based page. A page of zero is read as the first page.
        /// </summary>
        protected static IQueryable<E> Paginate<E>(IQueryable<E> query, uint page, uint limit)
        {
            return query.Skip((int)((page < 1 ? 0 : page - 1) * limit)).Take((int)limit);
        }
    }
}
