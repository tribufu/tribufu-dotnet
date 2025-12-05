// Copyright (c) Tribufu. All Rights Reserved.
// SPDX-License-Identifier: MIT

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tribufu.Database.Repositories;

namespace Tribufu.Database
{
    public class DatabaseSeeder
    {
        private readonly IServiceProvider _provider;

        public DatabaseSeeder(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task SeedAsync()
        {
            var repoType = typeof(IRepository<,>);

            var repos = _provider
                .GetServices<object>()
                .Where(s =>
                {
                    var type = s.GetType();
                    return type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == repoType);
                })
                .ToList();

            foreach (var repo in repos)
            {
                var method = repo.GetType().GetMethod("SeedAsync");

                if (method != null)
                {
                    var task = method.Invoke(repo, null);

                    if (task != null)
                    {
                        await (Task)task;
                    }
                }
            }
        }
    }
}
