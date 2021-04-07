using dapper_sqlite_demo.Database;
using dapper_sqlite_demo.LogMaster;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace dapper_sqlite_demo_tests
{
    public class LogProviderTest
    {
        private readonly IDbConnection _connection;
        public LogProviderTest()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            _connection = connection;
        }

        [Fact]
        public async Task LogProvider_Reads_Records_In_Db()
        {
            // arrange
            var logs = new List<Log>
            {
                new Log
                {
                    Id = 2, HttpVerb="GET", InsertDate= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), RequestBody = "myRequestBody", RequestHost="myRequestHost", RequestPath="myRequestPath", RequestQueryString="myRequestQueryString", ResponseBody = "myResponseBody", ResponseStatusCode = 200, User="mockedUser"
                } 
            };

            IDbBootstrap dbBootstrap = new DbBootstrap(_connection);
            await dbBootstrap.SetupAsync();
            await TestHelper.SeedDataInDbAsync(_connection, logs);

            // act
            ILogProvider logProvider = new LogProvider(_connection);
            var logsResult = await logProvider.Get();

            // assert
            Assert.Equal(JsonSerializer.Serialize(logs[0]), JsonSerializer.Serialize(logsResult.ToArray()[0]));
        }

        [Fact]
        public async Task LogProvider_Must_Return_Empty_When_No_Record_Present()
        {
            // arrange
            IDbBootstrap dbBootstrap = new DbBootstrap(_connection);
            await dbBootstrap.SetupAsync();

            // act
            ILogProvider logProvider = new LogProvider(_connection);
            var logsResult = await logProvider.Get();

            // assert
            Assert.Empty(logsResult);

        }
    }
}
