// Copyright (c) Tribufu. All Rights Reserved.
// SPDX-License-Identifier: UNLICENSED

using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Tribufu.AspNetCore.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetIpAddress(this HttpContext context)
        {
            var headers = context.Request.Headers;
            if (headers.TryGetValue("CF-Connecting-IP", out var cfConnectingIp))
            {
                return cfConnectingIp.FirstOrDefault() ?? "127.0.0.1";
            }

            if (headers.TryGetValue("X-Forwarded-For", out var xForwardedFor))
            {
                var forwardedIps = xForwardedFor.ToString().Split(',', StringSplitOptions.RemoveEmptyEntries);

                if (forwardedIps.Length > 0)
                {
                    return forwardedIps[0].Trim();
                }
            }

            return context.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";
        }
    }
}
