// Copyright (c) Tribufu. All Rights Reserved.
// SPDX-License-Identifier: MIT

using Newtonsoft.Json.Serialization;
using System.Text.RegularExpressions;

namespace Tribufu.Serialization
{
    public class NetworkAwareSnakeCaseNamingStrategy : SnakeCaseNamingStrategy
    {
        private static readonly string[] KnownAcronyms = new string[] { "IPv4", "IPv6" };

        protected override string ResolvePropertyName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }

            foreach (var acr in KnownAcronyms)
            {
                name = Regex.Replace(name, acr, acr.ToLower());
            }

            var snake = base.ResolvePropertyName(name);
            foreach (var acr in KnownAcronyms)
            {
                var lower = acr.ToLower();
                snake = snake.Replace(lower.Replace("_", ""), lower);
            }

            return snake;
        }
    }
}
