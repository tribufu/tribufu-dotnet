// Copyright (c) Tribufu. All Rights Reserved.
// SPDX-License-Identifier: MIT

namespace Tribufu
{
    /// <summary>
    /// Tribufu API
    /// </summary>
    /// <remarks>
    /// Helper class to get a singleton instance of the <see cref="TribufuApi"/>.
    /// </remarks>
    public static class TribufuApiSingleton
    {
        private static TribufuApi _instance = null;

        /// <summary>
        /// Get the singleton instance of <see cref="TribufuApi"/>.
        /// </summary>
        public static TribufuApi GetInstance()
        {
            if (_instance == null)
            {
                _instance = TribufuApi.FromEnvOrDefault();
            }

            return _instance;
        }

        /// <summary>
        /// Reset the singleton instance of <see cref="TribufuApi"/>.
        /// </summary>
        public static void ResetInstance()
        {
            _instance = null;
        }
    }
}
