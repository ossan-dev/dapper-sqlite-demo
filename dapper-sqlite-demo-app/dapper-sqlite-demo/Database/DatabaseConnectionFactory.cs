using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace dapper_sqlite_demo.Database
{
    public class DatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly DatabaseConfig _databaseConfig;

        public DatabaseConnectionFactory(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }
        public IDbConnection GetConnection() => new SqliteConnection(_databaseConfig.Name);
    }
}
