// Copyright (c) Tribufu. All Rights Reserved.
// SPDX-License-Identifier: MIT

using Microsoft.Extensions.Configuration;
using System;

namespace Tribufu.Database
{
    public class DatabaseConfiguration
    {
        public DatabaseDriver Driver { get; set; }

        public string? Version { get; set; }

        public string? Host { get; set; }

        public string? Port { get; set; }

        public string? User { get; set; }

        public string? Password { get; set; }

        public string? Schema { get; set; }

        /// <summary>
        /// Loads the <see cref="DatabaseConfiguration"/> from the "database" section or from root-level keys prefixed with "database_".
        /// </summary>
        /// <param name="configuration">The configuration source.</param>
        /// <returns>The populated <see cref="DatabaseConfiguration"/> instance.</returns>
        public static DatabaseConfiguration Load(IConfiguration configuration)
        {
            var section = configuration.GetSection("database");
            var useRootFallback = !section.Exists();

            string? GetConfig(string key) => useRootFallback ? configuration[$"database_{key}"] : section[key];

            var driverString = GetConfig("driver") ?? throw new Exception("Missing database driver");
            if (!Enum.TryParse<DatabaseDriver>(driverString, true, out var driver))
            {
                throw new Exception($"Unsupported database driver: {driverString}");
            }

            return new DatabaseConfiguration
            {
                Driver = driver,
                Version = GetConfig("version"),
                Host = GetConfig("host"),
                Port = GetConfig("port"),
                User = GetConfig("user"),
                Password = GetConfig("password"),
                Schema = GetConfig("schema")
            };
        }

        /*
        services.AddDbContext<DbContext>(options =>
        {
            switch (dbConfig.Driver)
            {
                case DatabaseDriver.MySql:
                    var mysqlConnection = $"Server={dbConfig.Host};Port={dbConfig.Port};Uid={dbConfig.User};Pwd={dbConfig.Password};Database={dbConfig.Schema};ConvertZeroDateTime=True;";
                    options.UseMySql(mysqlConnection, ServerVersion.Parse(dbConfig.Version ?? "8.0"), mySqlOptions => { });
                    break;
                case DatabaseDriver.Postgres:
                    var pgsqlConnection = $"Host={dbConfig.Host};Port={dbConfig.Port};Database={dbConfig.Schema};Username={dbConfig.User};Password={dbConfig.Password};";
                    options.UseNpgsql(pgsqlConnection, npgsqlOptions => { });
                    break;
                case DatabaseDriver.SqlServer:
                    var sqlServerConnection = $"Server={dbConfig.Host},{dbConfig.Port};Database={dbConfig.Schema};User Id={dbConfig.User};Password={dbConfig.Password};Encrypt=True;TrustServerCertificate=True;";
                    options.UseSqlServer(sqlServerConnection, sqlOptions => { });
                    break;
                case DatabaseDriver.Oracle:
                    var oracleConnection = $"User Id={dbConfig.User};Password={dbConfig.Password};Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={dbConfig.Host})(PORT={dbConfig.Port})))(CONNECT_DATA=(SERVICE_NAME={dbConfig.Schema})));";
                    options.UseOracle(oracleConnection, oracleOptions => { });
                    break;
                case DatabaseDriver.Firebird:
                    var firebirdConnection = $"User={dbConfig.User};Password={dbConfig.Password};Database={dbConfig.Host}:{dbConfig.Port}/{dbConfig.Schema};Dialect=3;";
                    options.UseFirebird(firebirdConnection, firebirdOptions => { });
                    break;
                case DatabaseDriver.Sqlite:
                    var savedDirectory = Paths.GetApplicationSavedDirectory();
                    if (!Directory.Exists(savedDirectory)) Directory.CreateDirectory(savedDirectory);
                    var sqliteDatabaseFile = string.IsNullOrEmpty(dbConfig.Schema) ? "default.db" : $"{dbConfig.Schema}.db";
                    var sqliteDatabasePath = Path.Combine(savedDirectory, sqliteDatabaseFile);
                    options.UseSqlite($"Data Source={sqliteDatabasePath}", sqliteOptions => { });
                    break;
                case DatabaseDriver.MongoDb:
                    var mongoUriBuilder = new MongoUrlBuilder
                    {
                        Server = new MongoServerAddress(dbConfig.Host, int.Parse(dbConfig.Port ?? "27017")),
                        Username = dbConfig.User,
                        Password = dbConfig.Password,
                        DatabaseName = dbConfig.Schema
                    };
                    var mongoClient = new MongoClient(mongoUriBuilder.ToMongoUrl());
                    var mongoDatabase = mongoClient.GetDatabase(dbConfig.Schema ?? "default");
                    options.UseMongoDB(mongoDatabase.Client, mongoDatabase.DatabaseNamespace.DatabaseName);
                    break;
                default:
                    throw new NotSupportedException($"Unsupported database driver: {dbConfig.Driver}");
            }
        });
        */
    }
}
