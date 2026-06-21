// Copyright (c) Tribufu. All Rights Reserved.
// SPDX-License-Identifier: MIT

using Microsoft.Extensions.Configuration;

namespace Tribufu.Configuration.Extensions
{
    public static class IConfigurationExtensions
    {
        public static string? Get(this IConfiguration configuration, string prefix, string key)
        {
            var section = configuration.GetSection(prefix);
            return !section.Exists() ? configuration[$"{prefix}_{key}"] : section[key];
        }
    }
}
