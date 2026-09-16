// Copyright (c) Tribufu. All Rights Reserved.
// SPDX-License-Identifier: MIT

using Newtonsoft.Json;
using System;

namespace Tribufu.Serialization
{
    public class ULongNullableStringConverter : JsonConverter<ulong?>
    {
        public override ulong? ReadJson(JsonReader reader, Type objectType, ulong? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            if (reader.TokenType == JsonToken.String || reader.TokenType == JsonToken.Integer)
            {
                string value = reader.Value?.ToString();

                if (string.IsNullOrWhiteSpace(value))
                {
                    return null;
                }

                return ulong.Parse(value);
            }

            throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing ulong?.");
        }

        public override void WriteJson(JsonWriter writer, ulong? value, JsonSerializer serializer)
        {
            if (value.HasValue)
            {
                writer.WriteValue(value.Value.ToString());
            }
            else
            {
                writer.WriteNull();
            }
        }
    }
}
