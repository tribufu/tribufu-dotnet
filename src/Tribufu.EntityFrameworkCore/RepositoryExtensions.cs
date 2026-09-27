// Copyright (c) Tribufu. All Rights Reserved.
// SPDX-License-Identifier: MIT

using System.Threading.Tasks;

namespace Tribufu.EntityFrameworkCore
{
    public static class RepositoryExtensions
    {
        /// <summary>
        /// Find an entity where its absence is an expected outcome rather than an error, such as while checking
        /// credentials.
        /// </summary>
        public static Task<T?> FindOrDefaultAsync<T, K>(this IRepository<T, K> repository, K key) where T : class
        {
            return repository.FindAsync(key).OrDefaultIfNotFound();
        }

        /// <summary>
        /// Await a lookup and answer null instead of throwing when the entity does not exist.
        /// </summary>
        public static async Task<T?> OrDefaultIfNotFound<T>(this Task<T> lookup) where T : class
        {
            try
            {
                return await lookup;
            }
            catch (EntityNotFoundException)
            {
                return null;
            }
        }
    }
}
