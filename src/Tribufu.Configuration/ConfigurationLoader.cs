// Copyright (c) Tribufu. All Rights Reserved.
// SPDX-License-Identifier: MIT

using Microsoft.Extensions.Configuration;
using System.IO;
using Tomlyn.Extensions.Configuration;
using Tribufu.Logging;
using Tribufu.Platform;

namespace Tribufu.Configuration
{
    public static class ConfigurationLoader
    {
        public static IConfiguration Load(string[] fileNames)
        {
            var configDirectory = Paths.GetApplicationConfigDirectory();
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddEnvironmentVariables();

            foreach (var fileName in fileNames)
            {
                var fullPath = Path.Combine(configDirectory, fileName);
                if (!File.Exists(fullPath))
                {
                    Logger.Debug($"Config file '{fullPath}' not found, skipping.");
                    continue;
                }

                var extension = Path.GetExtension(fullPath).ToLowerInvariant();
                switch (extension)
                {
                    case ".ini":
                        configurationBuilder.AddIniFile(fullPath, true, false);
                        break;
                    case ".json":
                        configurationBuilder.AddJsonFile(fullPath, true, false);
                        break;
                    case ".toml":
                        configurationBuilder.AddTomlFile(fullPath, true, false);
                        break;
                    default:
                        Logger.Warn($"Unsupported config file extension: {extension}");
                        break;
                }
            }

            return configurationBuilder.Build();
        }
    }
}
