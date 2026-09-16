// Copyright (c) Tribufu. All Rights Reserved.
// SPDX-License-Identifier: MIT

using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Tribufu.Serialization
{
    public class DecimalStringConverter : JsonConverter<decimal>
    {
        public override decimal ReadJson(JsonReader reader, Type objectType, decimal existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String && decimal.TryParse(reader.Value?.ToString(), out var result))
            {
                return result;
            }

            if (reader.TokenType == JsonToken.Integer)
            {
                return Convert.ToUInt64(reader.Value);
            }

            throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing decimal.");
        }

        public override void WriteJson(JsonWriter writer, decimal value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(CultureInfo.InvariantCulture));
        }
    }
}
