// Copyright (c) Tribufu. All Rights Reserved.
// SPDX-License-Identifier: UNLICENSED

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tribufu.Serialization
{
    public class BaseClassFirstContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var props = base.CreateProperties(type, memberSerialization);
            return props.OrderBy(p => !string.Equals(p.PropertyName, "id", StringComparison.OrdinalIgnoreCase)).ThenBy(p => p.DeclaringType != type).ToList();
        }
    }
}
