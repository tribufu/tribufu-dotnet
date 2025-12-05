// Copyright (c) Tribufu. All Rights Reserved.
// SPDX-License-Identifier: MIT

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tribufu.Database.Repositories
{
    public interface IRepository<T, K> where T : class
    {
        Task SeedAsync();

        IList<T> GetAll();

        Task<IList<T>> GetAllAsync();

        IList<T> GetPage(uint page, uint limit);

        Task<IList<T>> GetPageAsync(uint page, uint limit);

        T? GetOne(K key);

        Task<T?> GetOneAsync(K key);

        T? Create(T entity);

        Task<T?> CreateAsync(T entity);

        T? Update(T entity);

        Task<T?> UpdateAsync(T entity);

        void Delete(K key);

        Task DeleteAsync(K key);

        void Delete(T entity);

        Task DeleteAsync(T entity);
    }
}
