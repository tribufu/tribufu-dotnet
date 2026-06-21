// Copyright (c) Tribufu. All Rights Reserved.
// SPDX-License-Identifier: MIT

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tribufu.EntityFrameworkCore
{
    public interface IRepository<T, K> where T : class
    {
        void Seed();

        Task SeedAsync();

        IList<T> List();

        Task<IList<T>> ListAsync();

        IList<T> List(uint page, uint limit);

        Task<IList<T>> ListAsync(uint page, uint limit);

        T? Find(K key);

        Task<T?> FindAsync(K key);

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
