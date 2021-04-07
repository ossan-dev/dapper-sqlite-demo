using dapper_sqlite_demo.Database;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace dapper_sqlite_demo_tests
{
    public class DatabaseConnectionFactoryInMemory : IDatabaseConnectionFactory
    {
        public DatabaseConnectionFactoryInMemory(DatabaseConfig databaseConfig)
        {

        }

        public IDbConnection GetConnection() => new SqliteConnection("DataSource =:memory:");    
    }
}
