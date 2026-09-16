// Copyright (c) Tribufu. All Rights Reserved.
// SPDX-License-Identifier: MIT

using dotenv.net;
using System;
using System.Threading.Tasks;
using Tribufu.Client;
using Tribufu.Logging;

namespace Tribufu.Test
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            Logger.Initialize(LogLevel.Trace);

            DotEnv.Load(new DotEnvOptions(ignoreExceptions: true, envFilePaths: [".env", "../../.env"]));

            var apiKey = Environment.GetEnvironmentVariable("TRIBUFU_API_KEY");
            var tribufu = new TribufuApi(apiKey ?? "");

            Logger.Debug(TribufuApi.GetUserAgent());

            try
            {
                var result = await tribufu.GetUserInfoAsync();
                Logger.Debug(result.ToString());
            }
            catch (ApiException e)
            {
                Logger.Debug(e.Message);
            }
        }
    }
}
