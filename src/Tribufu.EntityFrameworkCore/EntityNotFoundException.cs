// Copyright (c) Tribufu. All Rights Reserved.
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;

namespace Tribufu.EntityFrameworkCore
{
    /// <summary>
    /// Thrown when an entity looked up by its key does not exist.
    /// </summary>
    public class EntityNotFoundException : KeyNotFoundException
    {
        public Type EntityType { get; }

        public object? Key { get; }

        public EntityNotFoundException(Type entityType, object? key)
            : base($"{entityType.Name} '{key}' was not found.")
        {
            EntityType = entityType;
            Key = key;
        }

        public EntityNotFoundException(Type entityType, string message) : base(message)
        {
            EntityType = entityType;
        }

        public static EntityNotFoundException For<T>(object? key) => new(typeof(T), key);
    }
}
