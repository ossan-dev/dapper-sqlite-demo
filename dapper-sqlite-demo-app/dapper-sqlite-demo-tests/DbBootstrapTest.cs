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

        //[Fact]
        //public async Task SelectCountQuery_Must_Return_Exact_Number()
        //{
        //    // arrange
        //    var logs = new List<Log>
        //    {
        //        new Log{ Id = 1, HttpVerb="GET", InsertDate= DateTime.Now.AddYears(-1), RequestBody = "{'requestBody': 'request from mock instance'}", RequestHost="mockedInstance", RequestPath="/mocked/api/", RequestQueryString="?instance=mocked", ResponseBody = "{'responseBody': 'response mocked by me'}", ResponseStatusCode = 200, User="mockedUser"},
        //        new Log{ Id = 2, HttpVerb="GET", InsertDate= DateTime.Now.AddYears(-2), RequestBody = "{'requestBody': 'request from mock instance'}", RequestHost="mockedInstance", RequestPath="/mocked/api/", RequestQueryString="?instance=mocked", ResponseBody = "{'responseBody': 'response mocked by me'}", ResponseStatusCode = 500, User="mockedUser"},
        //        new Log{ Id = 3, HttpVerb="GET", InsertDate= DateTime.Now.AddYears(-3), RequestBody = "{'requestBody': 'request from mock instance'}", RequestHost="mockedInstance", RequestPath="/mocked/api/", RequestQueryString="?instance=mocked", ResponseBody = "{'responseBody': 'response mocked by me'}", ResponseStatusCode = 400, User="mockedUser"}
        //    };

        //    //var connection = new SqliteConnection("DataSource=:memory:");
        //    IDbBootstrap dbBootstrap = new DbBootstrap(_connection);
        //    await dbBootstrap.SetupAsync();
        //    await TestHelper.SeedDataInDbAsync(_connection, logs);

        //    // act
        //    var queryResult = await _connection.ExecuteScalarAsync<int>(@"SELECT COUNT(Id) FROM log");

        //    // assert
        //    Assert.Equal(logs.Count, queryResult);
        //}
    }
}
