// Copyright (c) Tribufu. All Rights Reserved.
// SPDX-License-Identifier: MIT

using System.Collections.Generic;

namespace Tribufu.Database.Repositories
{
    public class RepositorySeeder
    {
        private readonly IEnumerable<IRepository> _repositories;

        public RepositorySeeder(IEnumerable<IRepository> repositories)
        {
            _repositories = repositories;
        }

        public void Run()
        {
            foreach (var repo in _repositories)
            {
                repo.SeedDefaults();
            }
        }
    }
}
