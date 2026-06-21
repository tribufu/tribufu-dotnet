// Copyright (c) Tribufu. All Rights Reserved.
// SPDX-License-Identifier: MIT

using System;
using System.IO;
using System.Runtime.InteropServices;
using Tribufu.Logging;

namespace Tribufu.Platform
{
    /// <summary>
    /// Provides standardized access to important directories, such as config, saved data, logs, and platform-specific binaries.
    /// This is especially useful for abstracting file path logic across environments (development, production, etc).
    /// </summary>
    public static class Paths
    {
        /// <summary>
        /// Gets the root base directory of the application.
        /// </summary>
        /// <remarks>
        /// - In development, this resolves to the root of the repository (five levels above bin/Debug or bin/Release).
        /// - In production, it resolves to two levels above the binary location.
        /// - It uses case-insensitive checks and runtime heuristics to improve accuracy.
        /// </remarks>
        /// <returns>The absolute path to the base directory.</returns>
        public static string GetApplicationDirectory()
        {
            try
            {
                string baseDirectory;
                string defaultBaseDirectory = AppContext.BaseDirectory;

                bool isDevelopment = defaultBaseDirectory.ToLowerInvariant().Contains("debug");

                if (isDevelopment)
                {
                    // Go 5 levels up to simulate project root
                    baseDirectory = Path.Combine(defaultBaseDirectory, "..", "..", "..", "..", "..");
                }
                else
                {
                    baseDirectory = Path.Combine(defaultBaseDirectory, "..", "..");
                }

                return Path.GetFullPath(baseDirectory);
            }
            catch (Exception ex)
            {
                Logger.Warn($"(Paths) Failed to resolve base directory: {ex.Message}");
                return AppContext.BaseDirectory;
            }
        }

        /// <summary>
        /// Gets the path to the platform-specific binary directory.
        /// </summary>
        /// <returns>
        /// The absolute path to <c>bin/&lt;runtime-identifier&gt;</c> if available,
        /// otherwise falls back to <c>bin/dotnet</c>.
        /// </returns>
        public static string GetApplicationBinDirectory()
        {
            var binDirectory = Path.Combine(GetApplicationDirectory(), "bin");

            if (!string.IsNullOrEmpty(RuntimeInformation.RuntimeIdentifier))
            {
                binDirectory = Path.Combine(binDirectory, RuntimeInformation.RuntimeIdentifier);
            }
            else
            {
                binDirectory = Path.Combine(binDirectory, "dotnet");
            }

            return binDirectory;
        }

        /// <summary>
        /// Gets the path to the configuration directory.
        /// </summary>
        /// <returns>The absolute path to the <c>config</c> directory.</returns>
        public static string GetApplicationConfigDirectory()
        {
            return Path.Combine(GetApplicationDirectory(), "config");
        }

        /// <summary>
        /// Gets the path to the assets directory.
        /// </summary>
        /// <returns>The absolute path to the <c>assets</c> directory.</returns>
        public static string GetApplicationAssetsDirectory()
        {
            return Path.Combine(GetApplicationDirectory(), "assets");
        }

        /// <summary>
        /// Gets the path to the saved data directory.
        /// </summary>
        /// <returns>The absolute path to the <c>saved</c> directory.</returns>
        public static string GetApplicationSavedDirectory()
        {
            return Path.Combine(GetApplicationDirectory(), "saved");
        }

        /// <summary>
        /// Gets the path to the cache directory inside <c>saved</c>.
        /// </summary>
        /// <returns>The absolute path to the <c>saved/cache</c> directory.</returns>
        public static string GetApplicationCacheDirectory()
        {
            return Path.Combine(GetApplicationSavedDirectory(), "cache");
        }

        /// <summary>
        /// Gets the path to the logs directory inside <c>saved</c>.
        /// </summary>
        /// <returns>The absolute path to the <c>saved/logs</c> directory.</returns>
        public static string GetApplicationLogsDirectory()
        {
            return Path.Combine(GetApplicationSavedDirectory(), "logs");
        }
    }
}
