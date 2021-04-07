using Dapper;
using dapper_sqlite_demo.Database;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace dapper_sqlite_demo_tests
{
    public class RequestResponseLogTest
    {
        private readonly IDbConnection _connection;

        public RequestResponseLogTest()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
        }

        [Fact]
        public async Task DatabaseBootstrap_Create_Table_When_Not_Exists()
        {
            // arrange 
            IDbBootstrap dbBootstrap = new DbBootstrap(_connection);
            await dbBootstrap.SetupAsync();

            // act


            // assert
        }

        //[Fact]
        //public async Task SelectCountQuery_Must_Return_Exact_Number()
        //{ 
        //// arrange
        //var logs = new List<RequestResponseLog>
        //{
        //    new RequestResponseLog{ Id = 1, HttpVerb="GET", InsertDate= DateTime.Now.AddYears(-1), RequestBody = "{'requestBody': 'request from mock instance'}", RequestHost="mockedInstance", RequestPath="/mocked/api/", RequestQueryString="?instance=mocked", ResponseBody = "{'responseBody': 'response mocked by me'}", ResponseStatusCode = 200, User="mockedUser"},
        //    new RequestResponseLog{ Id = 2, HttpVerb="GET", InsertDate= DateTime.Now.AddYears(-2), RequestBody = "{'requestBody': 'request from mock instance'}", RequestHost="mockedInstance", RequestPath="/mocked/api/", RequestQueryString="?instance=mocked", ResponseBody = "{'responseBody': 'response mocked by me'}", ResponseStatusCode = 500, User="mockedUser"},
        //    new RequestResponseLog{ Id = 3, HttpVerb="GET", InsertDate= DateTime.Now.AddYears(-3), RequestBody = "{'requestBody': 'request from mock instance'}", RequestHost="mockedInstance", RequestPath="/mocked/api/", RequestQueryString="?instance=mocked", ResponseBody = "{'responseBody': 'response mocked by me'}", ResponseStatusCode = 400, User="mockedUser"}
        //};

        //var connection = _databaseBootstrap.GetConnection();
        //await _databaseBootstrap.SetupAsync(connection);
        //await TestHelper.SeedDataInDbAsync(connection, logs);

        //// act
        //var queryResult = await connection.ExecuteScalarAsync<int>(@"SELECT COUNT(Id) FROM request_response_log");


        //// assert
        //Assert.Equal(logs.Count, queryResult);
        //}
    }
}
