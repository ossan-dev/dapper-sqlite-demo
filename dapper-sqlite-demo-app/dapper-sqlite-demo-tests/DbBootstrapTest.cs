using Dapper;
using dapper_sqlite_demo.Database;
using dapper_sqlite_demo.LogMaster;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace dapper_sqlite_demo_tests
{
    public class DbBootstrapTest
    {
        private readonly IDbConnection _connection;

        public DbBootstrapTest()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            _connection = connection;
        }

        [Fact]
        public async Task DatabaseBootstrap_Create_Table_When_Not_Exists()
        {
            // arrange 
            IDbBootstrap dbBootstrap = new DbBootstrap(_connection);
            await dbBootstrap.SetupAsync();

            // act
            var tables = await _connection.QueryAsync<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'log';");

            // assert
            Assert.Equal("log", tables.FirstOrDefault());
        }

        [Fact]
        public async Task DatabaseBootstrap_Run_Twice_But_Create_One_Table()
        {
            // arrange
            IDbBootstrap dbBootstrap = new DbBootstrap(_connection);
            await dbBootstrap.SetupAsync();
            await dbBootstrap.SetupAsync();

            // act
            var tables = await _connection.QueryAsync<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'log';");

            // assert
            Assert.Single(tables);
            Assert.Equal("log", tables.FirstOrDefault());
        }        
    }
}
