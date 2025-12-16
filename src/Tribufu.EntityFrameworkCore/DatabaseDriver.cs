// Copyright (c) Tribufu. All Rights Reserved.
// SPDX-License-Identifier: MIT

namespace Tribufu.EntityFrameworkCore
{
    public enum DatabaseDriver : byte
    {
        MySql = 0,

        Postgres = 1,

        SqlServer = 2,

        Oracle = 3,

        Firebird = 4,

        Sqlite = 5,

        MongoDb = 6,
    }
}
