using Dapper;
using dapper_sqlite_demo.Database;
using dapper_sqlite_demo.RequestResponseLogMaster;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace dapper_sqlite_demo_tests
{
    public class RequestResponseLogTest
    {
        private readonly IDatabaseBootstrap _databaseBootstrap;

        public RequestResponseLogTest()
        {
            _databaseBootstrap = new InMemoryDatabaseBootstrap(new DatabaseConfig() { Name = ""});
        }

        [Fact]
        public async Task SelectCountQuery_Must_Return_Exact_Number()
        { 
            // arrange
            var logs = new List<RequestResponseLog>
            {
                new RequestResponseLog{ Id = 1, HttpVerb="GET", InsertDate= DateTime.Now.AddYears(-1), RequestBody = "{'requestBody': 'request from mock instance'}", RequestHost="mockedInstance", RequestPath="/mocked/api/", RequestQueryString="?instance=mocked", ResponseBody = "{'responseBody': 'response mocked by me'}", ResponseStatusCode = 200, User="mockedUser"},
                new RequestResponseLog{ Id = 2, HttpVerb="GET", InsertDate= DateTime.Now.AddYears(-2), RequestBody = "{'requestBody': 'request from mock instance'}", RequestHost="mockedInstance", RequestPath="/mocked/api/", RequestQueryString="?instance=mocked", ResponseBody = "{'responseBody': 'response mocked by me'}", ResponseStatusCode = 500, User="mockedUser"},
                new RequestResponseLog{ Id = 3, HttpVerb="GET", InsertDate= DateTime.Now.AddYears(-3), RequestBody = "{'requestBody': 'request from mock instance'}", RequestHost="mockedInstance", RequestPath="/mocked/api/", RequestQueryString="?instance=mocked", ResponseBody = "{'responseBody': 'response mocked by me'}", ResponseStatusCode = 400, User="mockedUser"}
            };

            var connection = _databaseBootstrap.GetConnection();
            await _databaseBootstrap.SetupAsync(connection);
            await TestHelper.SeedDataInDbAsync(connection, logs);

            // act
            var queryResult = await connection.ExecuteScalarAsync<int>(@"SELECT COUNT(Id) FROM request_response_log");


            // assert
            Assert.Equal(logs.Count, queryResult);
        }
    }
}
