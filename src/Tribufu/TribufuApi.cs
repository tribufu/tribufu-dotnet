// Copyright (c) Tribufu. All Rights Reserved.
// SPDX-License-Identifier: MIT

using System;
using System.Net;
using System.Runtime.InteropServices;
using Tribufu.Api;
using Tribufu.Client;

namespace Tribufu
{
    /// <summary>
    /// Tribufu API
    /// </summary>
    /// <remarks>
    /// Use this class to interact with the Tribufu API.
    /// </remarks>
    public sealed class TribufuApi : TribufuGeneratedApi
    {
        /// <summary>
        /// The default base URL for the Tribufu API.
        /// </summary>
        public const string DefaultBaseUrl = "https://api.tribufu.com";

        /// <summary>
        /// Create a <see cref="TribufuApi"/> instance.
        /// </summary>
        public TribufuApi(string apiKey = null) : base(CreateConfiguration(apiKey))
        {
        }

        /// <summary>
        /// Create a TribufuApi with the default options.
        /// </summary>
        /// <returns>TribufuApi instance with default configuration</returns>
        public static TribufuApi Default()
        {
            return new TribufuApi();
        }

        /// <summary>
        /// Create a <see cref="TribufuApi"/> with the given API key.
        /// </summary>
        /// <remarks>
        /// An API key gives you public read only access to the Tribufu API.
        /// </remarks>
        /// <param name="apiKey">The API key for authentication</param>
        /// <returns><see cref="TribufuApi"/> instance configured with the API key</returns>
        public static TribufuApi WithApiKey(string apiKey)
        {
            return new TribufuApi(apiKey);
        }

        /// <summary>
        /// Try to create a <see cref="TribufuApi"/> from environment variables.
        /// </summary>
        /// <remarks>
        /// This will only work if the environment variables are set.
        /// </remarks>
        /// <param name="prefix">A prefix for the environment variables. Default is "TRIBUFU".</param>
        /// <returns><see cref="TribufuApi"/> instance or null if environment variables not set</returns>
        /// <example>
        /// // Environment variable TRIBUFU_API_KEY must be set
        /// var api = TribufuApi.FromEnv();
        /// </example>
        public static TribufuApi FromEnv(string prefix = null)
        {
            if (prefix == null)
            {
                prefix = "TRIBUFU";
            }

            var apiKey = Environment.GetEnvironmentVariable($"{prefix}_API_KEY");

            if (!string.IsNullOrEmpty(apiKey))
            {
                return WithApiKey(apiKey);
            }

            return null;
        }

        /// <summary>
        /// Create a <see cref="TribufuApi"/> from environment variables or the default API.
        /// </summary>
        /// <remarks>
        /// This will fallback to the default API if the environment variables are not set.
        /// </remarks>
        /// <param name="prefix">A prefix for the environment variables. Default is "TRIBUFU".</param>
        /// <returns><see cref="TribufuApi"/> instance</returns>
        /// <example>
        /// // Environment variable TRIBUFU_API_KEY might be unset
        /// var api = TribufuApi.FromEnvOrDefault();
        /// </example>
        public static TribufuApi FromEnvOrDefault(string prefix = "TRIBUFU")
        {
            return FromEnv(prefix) ?? Default();
        }

        /// <summary>
        /// Gets the version of the Tribufu API client.
        /// </summary>
        public static string GetVersion()
        {
            var version = typeof(TribufuApi).Assembly.GetName().Version;
            return $"{version?.Major}.{version?.Minor}.{version?.Build}";
        }

        /// <summary>
        /// Gets the user agent string for the Tribufu API client.
        /// </summary>
        public static string GetUserAgent()
        {
            var version = GetVersion();
            var frameworkDescription = RuntimeInformation.FrameworkDescription.Trim();
            //var runtimeIdentifier = RuntimeInformation.RuntimeIdentifier.Trim();
            return $"Tribufu/{version} ({frameworkDescription})"; //; {runtimeIdentifier})";
        }

        /// <summary>
        /// Checks if debug mode is enabled.
        /// </summary>
        /// <returns>True if debug mode is enabled, otherwise false</returns>
        public static bool DebugEnabled()
        {
#if DEBUG
            return true;
#else
            return  false;
#endif
        }

        /// <summary>
        /// Get the base URL for the Tribufu API.
        /// </summary>
        /// <remarks>
        /// The base URL can be set using the environment variable TRIBUFU_API_URL.
        /// The custom base URL is only used if debug mode is enabled.
        /// The default base URL is https://api.tribufu.com.
        /// </remarks>
        /// <returns>Base URL string</returns>
        private static string GetBaseUrl()
        {
            var baseUrl = Environment.GetEnvironmentVariable("TRIBUFU_API_URL");

            if (DebugEnabled() && !string.IsNullOrEmpty(baseUrl))
            {
                return baseUrl;
            }

            return DefaultBaseUrl;
        }

        /// <summary>
        /// Creates a configuration for the Tribufu API client.
        /// </summary>
        private static Configuration CreateConfiguration(string apiKey)
        {
            var config = new Configuration
            {
                BasePath = GetBaseUrl(),
                UserAgent = WebUtility.UrlEncode(GetUserAgent()),
            };

            if (!string.IsNullOrEmpty(apiKey))
            {
                config.AddApiKeyPrefix("Authorization", "ApiKey");
                config.AddApiKey("Authorization", apiKey);
            }

            return config;
        }
    }
}
