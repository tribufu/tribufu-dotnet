// Copyright (c) Tribufu. All Rights Reserved.
// SPDX-License-Identifier: MIT

using Newtonsoft.Json;
using System;

namespace Tribufu.Serialization
{
    public class ULongStringConverter : JsonConverter<ulong>
    {
        public override ulong ReadJson(JsonReader reader, Type objectType, ulong existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String && ulong.TryParse(reader.Value?.ToString(), out var result))
            {
                return result;
            }

            if (reader.TokenType == JsonToken.Integer)
            {
                return Convert.ToUInt64(reader.Value);
            }

            throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing ulong.");
        }

        public override void WriteJson(JsonWriter writer, ulong value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}
