// Copyright (c) Tribufu. All Rights Reserved.
// SPDX-License-Identifier: MIT

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Tribufu.Serialization
{
    public static class SerializationConfiguration
    {
        public static JsonSerializerSettings GetNewtonsoftJsonSerializerSettings()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new BaseClassFirstContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
            };

            settings.Converters.Add(new DecimalNullableStringConverter());
            settings.Converters.Add(new DecimalStringConverter());
            settings.Converters.Add(new ULongNullableStringConverter());
            settings.Converters.Add(new ULongStringConverter());
            settings.Converters.Add(new StringEnumConverter(new SnakeCaseNamingStrategy(), false));

            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            return settings;
        }
    }
}
